using System;
using System.Web.Http;
using System.Web.Http.Cors;
using AkijRest.IdentityServer.Repository.Repositories;
using AkijRest.SolutionConstant;

namespace AkijRest.IdentityServer.Controllers
{
    [EnableCors(origins:UrlConstant.WebClient,headers:"*",methods:"*")]
    [RoutePrefix("api/userAuth")]
    public class UserAuthController : ApiController
    {
        [Route("gmail/{gmail}")]
        [HttpGet]
        public IHttpActionResult GetUserNameByGoogleEmail(string gmail)
        {
            //var claimsPrincipal = this.User as ClaimsPrincipal;

            //var username = claimsPrincipal?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            try
            {
                UserRepository userRepository = new UserRepository();
                var userName = userRepository.GetUserNameByGoogleEmail(gmail);
                return Ok(userName);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }


        [Route("facebook/{facebookMail}")]
        [HttpGet]
        public IHttpActionResult GetUserNameByFacebookEmail(string facebookMail)
        {
            //var claimsPrincipal = this.User as ClaimsPrincipal;

            //var username = claimsPrincipal?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            try
            {
                UserRepository userRepository = new UserRepository();
                var userName = userRepository.GetUserNameByFacebookEmail(facebookMail);
                return Ok(userName);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
