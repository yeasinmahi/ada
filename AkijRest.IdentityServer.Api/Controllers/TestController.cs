using AkijRest.SolutionConstant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AkijRest.IdentityServer.Api.Controllers
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