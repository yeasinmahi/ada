using AkijRest.IdentityServer.Repository.Models;
using AkijRest.IdentityServer.Repository.Repositories;
using AkijRest.IdentityServerFixed.Helpers;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace AkijRest.IdentityServerFixed.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        private UserRepository repository;

        public CustomOAuthProvider()
        {
            repository = new UserRepository();
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            string symmetricKeyAsBase64 = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id is not set");
                return Task.FromResult<object>(null);
            }

            var audience = AudienceStore.FindAudience(context.ClientId);

            if (audience == null)
            {
                context.SetError("invalid_clientId", string.Format("Invalid Client_id '{0}'", context.ClientId));
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add
            (
                "Access-Control-Allow-Origin",
                new[]
                {
                    allowedOrigin
                }
            );

            // Db Check Started
            User user = repository.Get(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name/password is incorrect");
                //return;
                return Task.FromResult<object>(null);
            }
            else
            {
                var identity = new ClaimsIdentity("JWT");

                /*
                 * // static roles before
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, "Manager"));
                identity.AddClaim(new Claim(ClaimTypes.Role, "Supervisor"));
                */

                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim("sub", context.UserName));

                /* For security reasons, we shall not include roles in access tokens, we shall find them in apicontroller
                 // against username
                RoleRepository roleRepository = new RoleRepository();

                var rolesAssigned = roleRepository.GetRolesByUserName(context.UserName);

                foreach (var role in rolesAssigned)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
                }
                */

                var props = new AuthenticationProperties
                (
                    new Dictionary<string, string>
                    {
                    {
                        "audience", (context.ClientId == null) ? string.Empty : context.ClientId
                    }
                    }
                );

                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);
                return Task.FromResult<object>(null);
            }

            /*
            if ( context.UserName != context.Password )
            {
                context.SetError("invalid_grant", "The user name/password is incorrect");
                //return;
                return Task.FromResult<object>(null);
            }

            var identity = new ClaimsIdentity("JWT");

            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Manager"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Supervisor"));

            var props = new AuthenticationProperties
            (
                new Dictionary<string, string>
                {
                    {
                        "audience", (context.ClientId == null) ? string.Empty : context.ClientId
                    }
                }
            );

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
            return Task.FromResult<object>(null);
            */

        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            return base.ValidateClientRedirectUri(context);
        }
    }
}