using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using AkijRest.IdentityServer.ApiFixed.Helpers.Roles;
using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Repositories;
using AkijRest.SolutionConstant;

namespace AkijRest.IdentityServer.ApiFixed.Controllers
{
    [EnableCors(origins: UrlConstant.WebClient, headers: "*", methods: "*")]
    [RoutePrefix("api/roles")]
    public class RoleController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            //var claimsPrincipal = this.User as ClaimsPrincipal;

            //var username = claimsPrincipal?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            try
            {
                RoleRepository roleRepository = new RoleRepository();

                List<RoleDto> listRoleDto = roleRepository.Get();

                return Ok(listRoleDto);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }


        [HttpPost]
        [Route("")]        
        public IHttpActionResult Post([FromBody]RoleUpdateDto dto)
        {
            try
            {
                RoleRepository roleRepository = new RoleRepository();
                roleRepository.UpdateRoles(dto.UserName, dto.StatusData);
                return Ok("skeleton");

            }
            catch(Exception ex)
            {
                return InternalServerError();
            }
        }


        [HttpGet]
        [Route("username/{userName}")]

        public IHttpActionResult GetRolesByUserName(string userName)
        {
            try
            {
                RoleRepository roleRepository = new RoleRepository();

                List<RoleDto> listRoleDto = roleRepository.GetRolesByUserName(userName);

                List<RoleAssignedDto> allRolesWithStatus = Helper.CombineWithAllRoles(listRoleDto);

                return Ok(allRolesWithStatus);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }



    }
}
