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
        [Route("insert")]
        public IHttpActionResult PostOwn([FromBody] LeaveDto dto)
        {
            Log.Instance.Write(logFilePath, "LeaveOwn", LogUtility.MessageType.MethodeStart);
            var claimsPrincipal = User as ClaimsPrincipal;
            var userName = ClaimsPrincipalHelper.ExtractUserName(claimsPrincipal);
            
            Log.Instance.Write(logFilePath, "UserName: " +userName, LogUtility.MessageType.UserMessage);
            TokenRepository tokenRepository = new TokenRepository();
            var tokenContent = tokenRepository.GetToken(userName);
            Log.Instance.Write(logFilePath, "tokenContent: " + tokenContent, LogUtility.MessageType.UserMessage);
            // this status gets the value whether the token is expired or refreshed
            string tokenRefeshStatus = tokenRepository.UpdateToken(tokenContent);
            Log.Instance.Write(logFilePath, "tokenRefeshStatus: " + tokenRefeshStatus, LogUtility.MessageType.UserMessage);
            if (tokenRefeshStatus.Equals("expired"))
            {
                // initializing logout functinality
                tokenRepository.DeleteToken(tokenContent);
                Log.Instance.Write(logFilePath, "deleted token content: ", LogUtility.MessageType.UserMessage);
                return Ok("expired");
            }
            RoleRepository roleRepository = new RoleRepository();
            var userRoles = roleRepository.GetRoleNamesByUserName(userName);
            string a = "";
            foreach (var userRole in userRoles)
            {
                a += userRole + "|";
            }
            Log.Instance.Write(logFilePath, a, LogUtility.MessageType.UserMessage);
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
                bool isAdmin = !String.IsNullOrWhiteSpace(dto.UserName);
                if (!isAdmin)
                {
                    dto.UserName = userName;
                }
                List<int> leaveIds = repository.Create(dto);
                if (leaveIds.Count > 0)
                {
                    foreach (int leaveId in leaveIds)
                    {
                        Log.Instance.Write(logFilePath, "leaveDto: object " + leaveId, LogUtility.MessageType.UserMessage);
                        string htmlInit = "<html><body><div>";
                        string htmlEnd = "</div></body></html>";
                        UserRepository userRepository = new UserRepository();
                        UserDto supervisorDto = userRepository.GetSuppervisorEmailByUserName(userName);
                        if (supervisorDto != null)
                        {
                            Log.Instance.Write(logFilePath, "userDto: object " + leaveId, LogUtility.MessageType.UserMessage);
                            EmailOptions emailOptions = new EmailOptions
                            {
                                ToAddressDisplayName = "Yeasin",
                                ToAddress = supervisorDto.Email,
                                Body = htmlInit + "Please Clik the link : <a href='" + UrlConstant.WebClient + "/Home/signIn?redirectUrl=" + UrlConstant.WebClient + "/Home/Test'>Test Page</a></div>" + htmlEnd,
                                Subject = "Test"
                            };
                            Email.Send(emailOptions);
                            Log.Instance.Write(logFilePath, "Mail sent in LeaveController " + leaveId, LogUtility.MessageType.UserMessage);
                        }
                        else
                        {
                            //todo: UserDto null
                        }
                        if (isAdmin)
                        {
                            UserDto userDto = userRepository.GetByUserName(dto.UserName);
                            if (userDto!=null)
                            {
                                EmailOptions emailOptions = new EmailOptions
                                {
                                    ToAddressDisplayName = "Yeasin",
                                    ToAddress = userDto.Email,
                                    Body = htmlInit + "Your leave is assigned by "+User.Identity.Name+" \\nPlease Clik the link : <a href='" + UrlConstant.WebClient + "/Home/signIn?redirectUrl=" + UrlConstant.WebClient + "/Home/Test'>Test Page</a> to see your leave</div>" + htmlEnd,
                                    Subject = "Test"
                                };
                                Email.Send(emailOptions);
                            }
                        }
                    }
                    
                }
                //Log.Instance.Write(logFilePath, "leaveDto: " + leaveId, LogUtility.MessageType.UserMessage);
                var result = Created<LeaveDto>(Request.RequestUri, dto);
                Log.Instance.Write(logFilePath, "result: " + result, LogUtility.MessageType.UserMessage);
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
            var claimsPrincipal = User as ClaimsPrincipal;
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
                repository.Create(dto);
                var result = Created(Request.RequestUri, dto);
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
            var claimsPrincipal = User as ClaimsPrincipal;
            var userName = ClaimsPrincipalHelper.ExtractUserName(claimsPrincipal);
            RoleRepository roleRepository = new RoleRepository();
            string name = User.Identity.Name;
            
            var userRoles = roleRepository.GetRoleNamesByUserName(userName);
            if (!userRoles.Contains("ViewAllLeave"))
            {
                return Content(HttpStatusCode.Forbidden, "Sorry, you are not allowed to perform this action");
            }

            try
            {
                LeaveRepository repository = new LeaveRepository();
                var leaves = repository.GetLeaveByUserName(name);
                return Ok(leaves);
            }
            catch(Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAll()
        {
            var claimsPrincipal = User as ClaimsPrincipal;
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
                var leaves = repository.Get();
                return Ok(leaves);

            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("all/{userName}")]
        public IHttpActionResult GetAll(string userName)
        {
            var claimsPrincipal = User as ClaimsPrincipal;
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
                return Ok(leaves);

            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [Route("update")]
        [HttpPost]
        public IHttpActionResult Update([FromBody] LeaveDto leaveDto)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(leaveDto.UserName))
                {
                    leaveDto.UserName = User.Identity.Name;
                }
                LeaveRepository repository = new LeaveRepository();
                int result = repository.Update(leaveDto);
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
                Log.Instance.Write(logFilePath, e.Message, LogUtility.MessageType.Exception);
                return InternalServerError();
            }
        }

    }
}
