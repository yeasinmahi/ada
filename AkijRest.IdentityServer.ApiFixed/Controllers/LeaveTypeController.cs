﻿using System;
using System.Web.Http;
using System.Web.Http.Cors;
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
            catch(Exception ex)
            {
                Log.Write(logFilePath, ex.Message, LogUtility.MessageType.Exception);
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
    }
}
