using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Repositories;
using AkijRest.SolutionConstant;
using LogService;

namespace AkijRest.IdentityServer.ApiFixed.Controllers
{
    [EnableCors(origins: UrlConstant.WebClient, headers: "*", methods: "*")]
    [RoutePrefix("api/leavetype")]
    public class LeaveTypeController : ApiController
    {
        string logFilePath = "C:/YeasinPublished/ada.txt";
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                Log.Write(logFilePath, "LeaveType", LogUtility.MessageType.MethodeStart);
                LeaveTypeRepository repository = new LeaveTypeRepository();
                var leaveTypeDtos = repository.Get();
                Log.Write(logFilePath, "LeaveType", LogUtility.MessageType.MethodeEnd);
                return Ok(leaveTypeDtos);

            }
            catch(Exception e)
            {
                Log.Write(logFilePath, e.Message, LogUtility.MessageType.Exception);
                return InternalServerError();
            }
        }
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Log.Write(logFilePath, "LeaveType", LogUtility.MessageType.MethodeStart);
                LeaveTypeRepository repository = new LeaveTypeRepository();
                var leaveTypeDtos = repository.Get(id);
                Log.Write(logFilePath, "LeaveType", LogUtility.MessageType.MethodeEnd);
                return Ok(leaveTypeDtos);

            }
            catch (Exception ex)
            {
                Log.Write(logFilePath, ex.Message, LogUtility.MessageType.Exception);
                return InternalServerError();
            }
        }
        [Route("insert")]
        [HttpPost]
        public IHttpActionResult Insert([FromBody] LeaveTypeDto leaveTypeDto)
        {
            try
            {
                LeaveTypeRepository repository = new LeaveTypeRepository();
                int result = repository.Create(leaveTypeDto);
                if (result>0)
                {
                    return Ok("Successfully Inserted");
                }
                else
                {
                    return Ok("Insert failed");
                }
            }
            catch (Exception e)
            {
                Log.Write(logFilePath, e.Message, LogUtility.MessageType.Exception);
                Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                return Content(HttpStatusCode.BadRequest, e.Message);
            }
        }
        [Route("update")]
        [HttpPost]
        public IHttpActionResult Update([FromBody] LeaveTypeDto leaveTypeDto)
        {
            try
            {
                LeaveTypeRepository repository = new LeaveTypeRepository();
                int result = repository.Update(leaveTypeDto);
                if (result > 0)
                {
                    return Ok("Successfully Updated");
                }
                else
                {
                    return Ok("Update failed");
                }
            }
            catch (Exception e)
            {
                Log.Write(logFilePath, e.Message, LogUtility.MessageType.Exception);
                return InternalServerError();
            }
        }
    }
}
