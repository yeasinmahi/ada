using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using AkijRest.IdentityServer.ApiFixed.Helpers.Roles;
using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Repositories;
using AkijRest.SolutionConstant;
using EmailService;
using LogService;

namespace AkijRest.IdentityServer.ApiFixed.Controllers
{
    [Authorize]
    [EnableCors(origins: UrlConstant.WebClient, headers: "*", methods: "*")]
    [RoutePrefix("api/leaves")]
    public class LeaveController : ApiController
    {
        string logFilePath = "C:/YeasinPublished/ada.txt";
        [HttpPost]
        [Route("own")]
        public IHttpActionResult PostOwn([FromBody] LeaveDto dto)
        {
            Log.Write(logFilePath, "LeaveOwn", LogUtility.MessageType.MethodeStart);
            var claimsPrincipal = this.User as ClaimsPrincipal;

            var userName = ClaimsPrincipalHelper.ExtractUserName(claimsPrincipal);

            Log.Write(logFilePath, "UserName: " +userName, LogUtility.MessageType.UserMessage);

            TokenRepository tokenRepository = new TokenRepository();

            var tokenContent = tokenRepository.GetToken(userName);
            Log.Write(logFilePath, "tokenContent: " + tokenContent, LogUtility.MessageType.UserMessage);
            // this status gets the value whether the token is expired or refreshed
            string tokenRefeshStatus = tokenRepository.UpdateToken(tokenContent);
            Log.Write(logFilePath, "tokenRefeshStatus: " + tokenRefeshStatus, LogUtility.MessageType.UserMessage);
            if (tokenRefeshStatus.Equals("expired"))
            {
                // initializing logout functinality
                tokenRepository.DeleteToken(tokenContent);
                //
                Log.Write(logFilePath, "deleted token content: ", LogUtility.MessageType.UserMessage);
                return Ok("expired");
            }

            RoleRepository roleRepository = new RoleRepository();

            var userRoles = roleRepository.GetRoleNamesByUserName(userName);

            string a = "";

            foreach (var userRole in userRoles)
            {
                a += userRole + "|";
            }

            Log.Write(logFilePath, a, LogUtility.MessageType.UserMessage);

            if (!userRoles.Contains("UpdateOwnLeave"))
            {
                return Content(HttpStatusCode.Forbidden, "Sorry, you are not allowed to perform this action");
            }

            try
            {
                LeaveRepository repository = new LeaveRepository();

                // Now as token is passed to the api, not username, 
                // for this own method, username will be null,
                // for this all method, username will be there

                dto.UserName = userName;
                LeaveDto leaveDto = repository.Create(dto);

                if (leaveDto!=null)
                {
                    Log.Write(logFilePath, "leaveDto: object " + leaveDto, LogUtility.MessageType.UserMessage);
                    string htmlInit = "<html><body><div>";
                    string htmlEnd = "</div></body></html>";
                    UserRepository userRepository = new UserRepository();
                    UserDto userDto = userRepository.GetSuppervisorEmailByUserName(userName);

                    if (userDto !=null)
                    {
                        Log.Write(logFilePath, "userDto: object " + leaveDto, LogUtility.MessageType.UserMessage);
                        EmailOptions emailOptions = new EmailOptions
                        {
                            ToAddressDisplayName = "Yeasin",
                            ToAddress = userDto.Email,
                            Body = htmlInit+ "Please Clik the link : <a href='"+ UrlConstant.WebClient + "/Home/signIn?redirectUrl="+ UrlConstant.WebClient + "/Home/Test'>Test Page</a></div>" + htmlEnd,
                            Subject = "Test"
                        };
                        Email.Send(emailOptions);
                        Log.Write(logFilePath, "Mail sent in LeaveController " + leaveDto, LogUtility.MessageType.UserMessage);
                    }
                    else
                    {
                        //todo: UserDto null
                    }
                    
                }
                Log.Write(logFilePath, "leaveDto: " + leaveDto, LogUtility.MessageType.UserMessage);
                var result = Created<LeaveDto>(Request.RequestUri
                    , dto);
                Log.Write(logFilePath, "result: " + result, LogUtility.MessageType.UserMessage);
                return result;

            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("all")]
        public IHttpActionResult PostAll([FromBody] LeaveDto dto)
        {
            var claimsPrincipal = this.User as ClaimsPrincipal;

            var userName = ClaimsPrincipalHelper.ExtractUserName(claimsPrincipal);

            RoleRepository roleRepository = new RoleRepository();

            var userRoles = roleRepository.GetRoleNamesByUserName(userName);


            if (!userRoles.Contains("UpdateAllLeave"))
            {
                return Content(HttpStatusCode.Forbidden, "Sorry, you are not allowed to perform this action");
            }

            try
            {
                LeaveRepository repository = new LeaveRepository();

                dto.UserName = userName;
                LeaveDto leaveDto = repository.Create(dto);

                var result = Created<LeaveDto>(Request.RequestUri
                    , dto);

                return result;

            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }


        [HttpGet]
        [Route("own")]
        public IHttpActionResult GetOwn()
        {
            var claimsPrincipal = this.User as ClaimsPrincipal;

            var userName = ClaimsPrincipalHelper.ExtractUserName(claimsPrincipal);

            RoleRepository roleRepository = new RoleRepository();

            var userRoles = roleRepository.GetRoleNamesByUserName(userName);


            if (!userRoles.Contains("ViewOwnLeave"))
            {
                return Content(HttpStatusCode.Forbidden, "Sorry, you are not allowed to perform this action");
            }

            try
            {
                LeaveRepository repository = new LeaveRepository();
                var leaves = repository.GetLeaveByUserName(userName);

                var leaveDict = new Dictionary<string, List<LeaveEveryDayDto>>();

                leaveDict.Add("data", leaves);

                return Ok(leaveDict);

            }
            catch(Exception ex)
            {
                return InternalServerError();
            }
        }


        [HttpGet]
        [Route("all/{userName}")]
        public IHttpActionResult GetAll(string userName)
        {
            var claimsPrincipal = this.User as ClaimsPrincipal;

            var userNameApiCaller = ClaimsPrincipalHelper.ExtractUserName(claimsPrincipal);

            RoleRepository roleRepository = new RoleRepository();

            var userRoles = roleRepository.GetRoleNamesByUserName(userNameApiCaller);


            if (!userRoles.Contains("ViewAllLeave"))
            {
                return Content(HttpStatusCode.Forbidden, "Sorry, you are not allowed to perform this action");
            }

            try
            {
                LeaveRepository repository = new LeaveRepository();
                var leaves = repository.GetLeaveByUserName(userName);

                var leaveDict = new Dictionary<string, List<LeaveEveryDayDto>>();

                leaveDict.Add("data", leaves);

                return Ok(leaveDict);

            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }


    }
}
