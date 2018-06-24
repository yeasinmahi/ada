using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using AkijRest.SolutionConstant;

namespace AkijRest.IdentityServer.ApiFixed.Controllers
{
    [EnableCors(origins: UrlConstant.WebClient, headers: "*", methods: "*")]
    [RoutePrefix("api/tests")]
    public class TestController: ApiController
    {
        [Authorize]
        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            var claimsPrincipal = this.User as ClaimsPrincipal;

            return
                Ok
                (
                    "success"
                );
        }
    }
}