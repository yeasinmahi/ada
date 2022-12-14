using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Models;
using AkijRest.IdentityServer.Repository.Repositories;
using AkijRest.SolutionConstant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AkijRest.IdentityServer.Api.Controllers
{
    [EnableCors(origins: UrlConstant.WebClient, headers: "*", methods: "*")]
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] UserDto userDto)
        {
            try
            {
                UserRepository repository = new UserRepository();

                User user = repository.Create(userDto);

                var result = Created<UserDto>(Request.RequestUri
                    + "/" + user.Id.ToString(), userDto);

                return result;

            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }


        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            //var claimsPrincipal = this.User as ClaimsPrincipal;

            //var username = claimsPrincipal?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            try
            {
                UserRepository userRepository = new UserRepository();

                List<UserDto> listUserDTO = userRepository.Get();

                return Ok(listUserDTO);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [Route("names")]
        [HttpGet]
        public IHttpActionResult GetOnlyUserNames()
        {
            //var claimsPrincipal = this.User as ClaimsPrincipal;

            //var username = claimsPrincipal?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            try
            {
                UserRepository userRepository = new UserRepository();

                List<UserDto> listUserDTO = userRepository.Get();

                string[] userNames = new string[listUserDTO.Count];

                for (int i = 0; i < userNames.Length; i++)
                {
                    userNames[i] = listUserDTO[i].UserName;
                }

                return Ok(userNames);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

    }

    




}
