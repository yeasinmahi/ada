using System;
using System.Web.Http;
using System.Web.Http.Cors;
using AkijRest.IdentityServer.Repository.Repositories;
using AkijRest.SolutionConstant;

namespace AkijRest.IdentityServer.ApiFixed.Controllers
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
