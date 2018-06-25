using AkijRest.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Web;
using AkijRest.SolutionConstant;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Thinktecture.IdentityModel.Tokens;

namespace AkijRest.IdentityServer.Helpers
{
    public class JWTToken
    {
        public static string GenerateToken(string userName)
        {
            string audienceId = AudienceConstant.ClientId;
            var identity = new ClaimsIdentity("JWT");

            /*
             * // static roles before
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Manager"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Supervisor"));
            */

            identity.AddClaim(new Claim(ClaimTypes.Name, userName));
            identity.AddClaim(new Claim("sub", userName));

            Audience audience = AudienceStore.FindAudience(AudienceConstant.ClientId);

            string symmetricKeyAsBase64 = audience.Base64Secret;

            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);

            var signingKey = new HmacSigningCredentials(keyByteArray);

            var issued = DateTime.Now;
            var expires = DateTime.Now.AddDays(1);

            var token = new JwtSecurityToken
            (
                UrlConstant.IdentityServer, audienceId, identity.Claims,
                issued, expires, signingKey
            );

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;

        }
    }
}