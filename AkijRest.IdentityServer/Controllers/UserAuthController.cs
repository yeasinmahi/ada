using System;
using System.Web.Http;
using System.Web.Http.Cors;
using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Repositories;
using AkijRest.SolutionConstant;

namespace AkijRest.IdentityServer.Controllers
{
    [EnableCors(origins:UrlConstant.WebClient,headers:"*",methods:"*")]
    [RoutePrefix("api/userAuth")]
    public class UserAuthController : ApiController
    {
        [Route("gmail")]
        [HttpPost]
        public IHttpActionResult GetUserNameByGoogleEmail([FromBody] UserDto userDto)
        {
            //var claimsPrincipal = this.User as ClaimsPrincipal;

            //var username = claimsPrincipal?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            try
            {
                UserRepository userRepository = new UserRepository();
                var userName = userRepository.GetUserNameByGoogleEmail(userDto.UserName);
                return Ok(userName);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }


        [Route("facebook")]
        [HttpPost]
        public IHttpActionResult GetUserNameByFacebookEmail([FromBody] UserDto userDto)
        {
            //var claimsPrincipal = this.User as ClaimsPrincipal;

            //var username = claimsPrincipal?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            try
            {
                UserRepository userRepository = new UserRepository();
                var userName = userRepository.GetUserNameByFacebookEmail(userDto.UserName);
                return Ok(userName);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
