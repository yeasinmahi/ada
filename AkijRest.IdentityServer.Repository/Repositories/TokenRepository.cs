using AkijRest.IdentityServer.Repository.Helpers.DbHelpers;
using AkijRest.IdentityServer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AkijRest.IdentityServer.Repository.Repositories
{
    public class TokenRepository
    {
        private IdentityServerDbContext context;
        private const int ExpireTime = 2; // in minutes

        public TokenRepository()
        {
            this.context = new IdentityServerDbContext();
        }

        public void InsertToken(string userName, string tokenContent)
        {
            /*
            if(TokenExists(userName))
            {
                UpdateToken(userName);
            }
            */
            //else
            {
                Token token = new Token();

                token.TokenContent = tokenContent;
                token.UserName = userName;
                token.TimeExpiry = DateTime.Now.AddMinutes(ExpireTime);

                context.Tokens.Add(token);

                context.SaveChanges();
            }
        }


        private bool TokenExists(string userName)
        {
            var token = context
                .Tokens
                .SingleOrDefault(
                    t => t.UserName.Equals(userName)
                );

            return !(token == null);
        }

        public string UpdateToken(string tokenContent)
        {
            Token token = context.Tokens.SingleOrDefault(t => t.TokenContent.Equals(tokenContent));

            // token time already expired
            // so don't update the token
            if ( token.TimeExpiry < DateTime.Now )
            {
                return "expired";
            }

            token.TimeExpiry = DateTime.Now.AddMinutes(ExpireTime);

            context.SaveChanges();

            return "refreshed";
        }

        public void DeleteToken(string tokenContent)
        {
            Token token = context.Tokens.SingleOrDefault(t => t.TokenContent.Equals(tokenContent));

            context.Tokens.Remove(token);

            context.SaveChanges();
        }

        public void DeleteTokenByUserName(string userName)
        {
            IEnumerable<Token> tokens = context
                .Tokens
                .ToList()
                .Where( t => t.UserName.Equals(userName));

            foreach( var token in tokens)
            {
                context.Tokens.Remove(token);
            }

            context.SaveChanges();
        }




        public string GetToken(string userName)
        {
            Token token = context.Tokens.SingleOrDefault(t => t.UserName.Equals(userName));
            return token.TokenContent;
        }


        public string GetUsernameByToken(string tokenContent)
        {
            Token token = context.Tokens.SingleOrDefault(t => t.TokenContent.Equals(tokenContent));
            return token.UserName;
        }



    }
    
}