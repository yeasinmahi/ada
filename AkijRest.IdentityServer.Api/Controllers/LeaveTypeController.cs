using AkijRest.IdentityServer.Repository.Repositories;
using AkijRest.SolutionConstant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AkijRest.IdentityServer.Api.Controllers
{
    [EnableCors(origins: UrlConstant.WebClient, headers: "*", methods: "*")]
    [RoutePrefix("api/leavetype")]
    public class LeaveTypeController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                LeaveTypeRepository repository = new LeaveTypeRepository();
                var leaveTypeDtos = repository.Get();
                return Ok(leaveTypeDtos);

            }
            catch(Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
