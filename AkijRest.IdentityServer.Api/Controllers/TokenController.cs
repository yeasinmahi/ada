using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace AkijRest.IdentityServer.Api.Controllers
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
