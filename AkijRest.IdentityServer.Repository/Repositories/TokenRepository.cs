using AkijRest.IdentityServer.Repository.Helpers.DbHelpers;
using AkijRest.IdentityServer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AkijRest.IdentityServer.Repository.Repositories
{
    public class TokenRepository
    {
        private readonly IdentityServerDbContext _context;
        private const int ExpireTime = 2; // in minutes
        public TokenRepository()
        {
            _context = new IdentityServerDbContext();
        }
        public int InsertToken(string userName, string tokenContent)
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
                _context.Tokens.Add(token);
                _context.SaveChanges();
                return token.Id;
            }
        }
        public bool TokenExists(string userName)
        {
            var token = _context.Tokens.FirstOrDefault(t => t.UserName.Equals(userName));
            return token != null;
        }
        public string UpdateToken(string tokenContent)
        {
            Token token = _context.Tokens.SingleOrDefault(t => t.TokenContent.Equals(tokenContent));
            // token time already expired
            // so don't update the token
            if ( token != null && token.TimeExpiry < DateTime.Now )
            {
                return "expired";
            }
            if (token != null) token.TimeExpiry = DateTime.Now.AddMinutes(ExpireTime);
            _context.SaveChanges();
            return "refreshed";
        }
        public void DeleteToken(string tokenContent)
        {
            Token token = _context.Tokens.SingleOrDefault(t => t.TokenContent.Equals(tokenContent));
            if (token != null) _context.Tokens.Remove(token);
            _context.SaveChanges();
        }
        public void DeleteTokenByUserName(string userName)
        {
            IEnumerable<Token> tokens = _context
                .Tokens
                .ToList()
                .Where( t => t.UserName.Equals(userName));

            foreach( var token in tokens)
            {
                _context.Tokens.Remove(token);
            }
            _context.SaveChanges();
        }
        public string GetToken(string userName)
        {
            Token token = _context.Tokens.SingleOrDefault(t => t.UserName.Equals(userName));
            return token.TokenContent;
        }
        public string GetUserNameByToken(string tokenContent)
        {
            Token token = _context.Tokens.SingleOrDefault(t => t.TokenContent.Equals(tokenContent));
            return token.UserName;
        }
    }
    
}