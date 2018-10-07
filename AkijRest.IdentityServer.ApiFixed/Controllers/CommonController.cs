using System.Net;
using System.Security.Claims;
using System.Web.Http;
using AkijRest.IdentityServer.ApiFixed.Helpers.Roles;
using AkijRest.IdentityServer.Repository.Repositories;

namespace AkijRest.IdentityServer.ApiFixed.Controllers
{
    public class CommonController : ApiController
    {
        public static bool CheckSecurity(string userName,string menu)
        {
            
            TokenRepository tokenRepository = new TokenRepository();
            var tokenContent = tokenRepository.GetToken(userName);
            // this status gets the value whether the token is expired or refreshed
            string tokenRefeshStatus = tokenRepository.UpdateToken(tokenContent);
            if (tokenRefeshStatus.Equals("expired"))
            {
                // initializing logout functinality
                tokenRepository.DeleteToken(tokenContent);
                return false;
            }
            RoleRepository roleRepository = new RoleRepository();
            var userRoles = roleRepository.GetRoleNamesByUserName(userName);
            string a = "";
            foreach (var userRole in userRoles)
            {
                a += userRole + "|";
            }
            if (!userRoles.Contains(menu))
            {
                return false;
            }
            return true;
        }
    }
}
