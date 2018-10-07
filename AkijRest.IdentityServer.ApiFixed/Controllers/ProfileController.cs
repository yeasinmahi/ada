using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using AkijRest.IdentityServer.ApiFixed.Helpers.Roles;
using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Repositories;
using AkijRest.SolutionConstant;
using EmailService;
using LogService;
using Newtonsoft.Json.Linq;

namespace AkijRest.IdentityServer.ApiFixed.Controllers
{
    [EnableCors(origins: UrlConstant.WebClient, headers: "*", methods: "*")]
    [RoutePrefix("api/profile")]
    public class ProfileController : ApiController
    {
        [HttpPost]
        [Route("update")]
        public IHttpActionResult PostOwn([FromBody] ProfileDto dto)
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            var userName = ClaimsPrincipalHelper.ExtractUserName(claimsPrincipal);
            CommonController.CheckSecurity(userName, "Profile");
            try
            {
                ProfileRepository repository = new ProfileRepository();
                if (repository.Update(dto)>0)
                {
                    var result = Created<ProfileDto>(Request.RequestUri, dto);
                    return result;
                }
                return Ok("failed");

            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

    }
}
