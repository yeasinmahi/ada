using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Repositories;
using AkijRest.SolutionConstant;

namespace AkijRest.IdentityServer.ApiFixed.Controllers
{
    [EnableCors(origins: UrlConstant.WebClient, headers: "*", methods: "*")]
    [RoutePrefix("api/rolegroups")]
    public class RoleGroupController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                RoleGroupRepository roleGroupRepository = new RoleGroupRepository();
                List<RoleGroupDto> roleGroupDtos = roleGroupRepository.Get();
                return Ok(roleGroupDtos);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
