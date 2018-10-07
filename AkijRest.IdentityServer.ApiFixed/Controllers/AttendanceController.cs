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
    [RoutePrefix("api/attendance")]
    public class AttendanceController : ApiController
    {
        string logFilePath = "C:/YeasinPublished/ada.txt";
        
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                Log.Instance.Write(logFilePath, "Attendance", LogUtility.MessageType.MethodeStart);
                AttendanceRepository repository = new AttendanceRepository();
                List<AttendanceDto> dtos = repository.Get();
                Log.Instance.Write(logFilePath, "Attendance", LogUtility.MessageType.MethodeEnd);
                return Ok(dtos);

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
                Log.Instance.Write(logFilePath, "Attendance", LogUtility.MessageType.MethodeStart);
                AttendanceRepository repository = new AttendanceRepository();
                AttendanceDto dto = repository.Get(id);
                Log.Instance.Write(logFilePath, "Attendance", LogUtility.MessageType.MethodeEnd);
                return Ok(dto);

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
                Log.Instance.Write(logFilePath, "Attendance", LogUtility.MessageType.MethodeStart);
                AttendanceRepository repository = new AttendanceRepository();
                List<AttendanceDto> dtos = repository.GetByEnroll(enroll);
                Log.Instance.Write(logFilePath, "Attendance", LogUtility.MessageType.MethodeEnd);
                //return Ok(mealDto);
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                Log.Instance.Write(logFilePath, ex.Message, LogUtility.MessageType.Exception);
                return null;
            }
        }
        [Route("getbydate")]
        [HttpGet]
        public IHttpActionResult GetByDate(DateTime date)
        {
            try
            {
                Log.Instance.Write(logFilePath, "Attendance", LogUtility.MessageType.MethodeStart);
                AttendanceRepository repository = new AttendanceRepository();
                List<AttendanceDto> dtos = repository.GetByDate(date);
                Log.Instance.Write(logFilePath, "Attendance", LogUtility.MessageType.MethodeEnd);
                //return Ok(mealDto);
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                Log.Instance.Write(logFilePath, ex.Message, LogUtility.MessageType.Exception);
                return null;
            }
        }
        [Route("getmonthlyattendance")]
        [HttpGet]
        public IHttpActionResult GetMonthlyAttendance(int enroll)
        {
            
            try
            {
                Log.Instance.Write(logFilePath, "Attendance", LogUtility.MessageType.MethodeStart);
                Log.Instance.Write("C:/YeasinPublished/adaTest.txt", "Test", LogUtility.MessageType.MethodeStart);
                DateTime date = DateTime.Now;
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                AttendanceRepository repository = new AttendanceRepository();
                List<AttendanceDto> dtos = repository.GetByEnrollAndDateRange(enroll, firstDayOfMonth, lastDayOfMonth);
                Log.Instance.Write(logFilePath, "Attendance", LogUtility.MessageType.MethodeEnd);
                //return Ok(mealDto);
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                Log.Instance.Write(logFilePath, ex.Message, LogUtility.MessageType.Exception);
                return null;
            }
        }
    }
}
