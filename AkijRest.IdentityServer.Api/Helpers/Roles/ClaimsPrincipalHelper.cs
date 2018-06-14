using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;

namespace AkijRest.IdentityServer.Api.Helpers.Roles
{
    public class ClaimsPrincipalHelper
    {
        public static string ExtractUserName(ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                var userName = claimsPrincipal?
                .FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                return userName;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public static List<string> ExtractUserRoles(ClaimsPrincipal claimsPrinciple)
        {
            return null;
        }

    }
}