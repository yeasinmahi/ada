using AkijRest.SolutionConstant;
using LogService;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Repositories;

namespace AkijRest.IdentityServer.ApiFixed.Controllers
{
    [EnableCors(origins: UrlConstant.WebClient, headers: "*", methods: "*")]
    [RoutePrefix("api/task")]
    public class TaskController : ApiController
    {
        string logFilePath = "C:/YeasinPublished/ada.txt";
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                Log.Instance.Write(logFilePath, "Task", LogUtility.MessageType.MethodeStart);
                TaskRepository repository = new TaskRepository();
                List<TaskDto> taskDtos = repository.Get();
                Log.Instance.Write(logFilePath, "Task", LogUtility.MessageType.MethodeEnd);
                return Ok(taskDtos);

            }
            catch (Exception ex)
            {
                Log.Instance.Write(logFilePath, ex.Message, LogUtility.MessageType.Exception);
                return new ExceptionResult(ex.InnerException, this);
            }
        }
        [Route("getbyid")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Log.Instance.Write(logFilePath, "Task", LogUtility.MessageType.MethodeStart);
                TaskRepository repository = new TaskRepository();
                TaskDto taskDto = repository.Get(id);
                Log.Instance.Write(logFilePath, "Task", LogUtility.MessageType.MethodeEnd);
                return Ok(taskDto);

            }
            catch (Exception ex)
            {
                Log.Instance.Write(logFilePath, ex.Message, LogUtility.MessageType.Exception);
                return new ExceptionResult(ex.InnerException, this);
            }
        }
        [Route("getbyenroll")]
        [HttpGet]
        public IHttpActionResult GetByEnroll(int enroll)
        {
            try
            {
                Log.Instance.Write(logFilePath, "Task", LogUtility.MessageType.MethodeStart);
                TaskRepository repository = new TaskRepository();
                List<TaskDto> taskDtos = repository.GetByEnroll(enroll);
                Log.Instance.Write(logFilePath, "Task", LogUtility.MessageType.MethodeEnd);
                return Ok(taskDtos);

            }
            catch (Exception ex)
            {
                Log.Instance.Write(logFilePath, ex.Message, LogUtility.MessageType.Exception);
                return new ExceptionResult(ex.InnerException, this);
            }
        }
    }
}
