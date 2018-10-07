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
    [RoutePrefix("api/meal")]
    public class MealController : ApiController
    {
        string logFilePath = "C:/YeasinPublished/ada.txt";
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                Log.Instance.Write(logFilePath, "Meal", LogUtility.MessageType.MethodeStart);
                MealRepository repository = new MealRepository();
                List<MealDto> mealDtos = repository.Get();
                Log.Instance.Write(logFilePath, "Meal", LogUtility.MessageType.MethodeEnd);
                return Ok(mealDtos);

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
                Log.Instance.Write(logFilePath, "Meal", LogUtility.MessageType.MethodeStart);
                MealRepository repository = new MealRepository();
                MealDto mealDto = repository.Get(id);
                Log.Instance.Write(logFilePath, "Meal", LogUtility.MessageType.MethodeEnd);
                return Ok(mealDto);

            }
            catch (Exception ex)
            {
                Log.Instance.Write(logFilePath, ex.Message, LogUtility.MessageType.Exception);
                return new ExceptionResult(ex.InnerException, this);
            }
        }
        [Route("mealbyday")]
        [HttpGet]
        public MealDto GetByDay(string day)
        {
            try
            {
                Log.Instance.Write(logFilePath, "Meal", LogUtility.MessageType.MethodeStart);
                MealRepository repository = new MealRepository();
                MealDto mealDto = repository.GetByDay(day);
                Log.Instance.Write(logFilePath, "Meal", LogUtility.MessageType.MethodeEnd);
                //return Ok(mealDto);
                return mealDto;
            }
            catch (Exception ex)
            {
                Log.Instance.Write(logFilePath, ex.Message, LogUtility.MessageType.Exception);
                return null;
            }
        }
    }
}
