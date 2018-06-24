using System;
using System.Web.Http;
using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Repositories;

namespace AkijRest.IdentityServer.ApiFixed.Controllers
{
    [RoutePrefix("api/token")]
    public class TokenController : ApiController
    {
        [HttpPost]
        [Route("refresh")]
        public IHttpActionResult Post([FromBody] TokenDto dto)
        {
            try
            {
                var tokenContent = dto.TokenContent;

                TokenRepository tokenRepository = new TokenRepository();

                // this status gets the value whether the token is expired or refreshed
                string tokenRefeshStatus =  tokenRepository.UpdateToken(tokenContent);

                if (tokenRefeshStatus.Equals("expired"))
                {
                    // initializing logout functinality
                    tokenRepository.DeleteToken(tokenContent);
                    //

                }

                return Ok(tokenRefeshStatus);

            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("roles")]
        public IHttpActionResult GetRolesByToken(string TokenContent)
        {
            try
            {
                TokenRepository tokenRepository = new TokenRepository();

                string userName = tokenRepository.GetUsernameByToken(TokenContent);

                RoleRepository roleRepository = new RoleRepository();

                var roles = roleRepository.GetRolesByUserName(userName);

                return Ok(roles);
            }
            catch(Exception ex)
            {
                return InternalServerError();
            }
        }

    }
}
