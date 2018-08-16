using System;
using System.Web.Http;
using System.Web.Http.Cors;
using AkijRest.IdentityServer.Helpers;
using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Repositories;
using AkijRest.SolutionConstant;
using LogService;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace AkijRest.IdentityServer.Controllers
{
    [EnableCors(origins: UrlConstant.WebClient, headers: "*", methods: "*")]
    [RoutePrefix("api/token")]
    public class TokenController : ApiController
    {
        string logFilePath = "C:/YeasinPublished/ada.txt";
        [HttpPost]
        [Route("")]
        public IHttpActionResult CustomToken([FromBody] UserDto userDto)
        {
            try
            {
                if (!userDto.UserName.EndsWith("@akij.net"))
                {
                    userDto.UserName += "@akij.net";
                }
                // Getting the token from the identity server
                Log.Write(logFilePath,"CustomToken", LogUtility.MessageType.MethodeStart);
                var client = new RestClient(UrlConstant.IdentityServer);

                var request = new RestRequest("oauth/token", Method.POST);

                request.AddParameter("client_id", "099153c2625149bc8ecb3e85e03f0022");
                request.AddParameter("grant_type", "password");
                request.AddParameter("username", userDto.UserName);
                request.AddParameter("password", userDto.Password);

                Log.Write(logFilePath, "Create Client", LogUtility.MessageType.MethodeStart);
                IRestResponse response = client.Execute(request);
                Log.Write(logFilePath, "Execute Client", LogUtility.MessageType.MethodeEnd);
                var content = response.Content;

                Log.Write(logFilePath, content, LogUtility.MessageType.UserMessage);
                // token fetch ends

                if (content.Contains("invalid_grant"))
                {
                    return Ok("invalid_grant");
                }


                // fetched token is inserted in the token table, with user name and expiry time

                JObject json = JObject.Parse(content);
                string tokenContent = json["access_token"].ToObject<string>();
                ExternalUserDto externalUserDto = new ExternalUserDto
                {
                    UserName = userDto.UserName,
                    AccessToken = tokenContent
                };
                InsertToken(externalUserDto);

                Log.Write(logFilePath, "CustomToken", LogUtility.MessageType.MethodeEnd);
                // the task ends

                // fake test
                //var jwtToken = JWTToken.GenerateToken(userDto.UserName);
                //

                return Ok(content);

            }
            catch (Exception ex)
            {
                Log.Write(logFilePath, ex.Message, LogUtility.MessageType.Exception);
                return InternalServerError();
            }
        }
        [Route("external")]
        public IHttpActionResult ExternalToken([FromBody] ExternalUserDto externalUserDto)
        {
            Log.Write(logFilePath, "ExternalToken", LogUtility.MessageType.MethodeStart);
            try
            {
                //externalUserDto.AccessToken = JWTToken.GenerateToken(externalUserDto.UserName);
                InsertToken(externalUserDto);
                Log.Write(logFilePath, "ExternalToken", LogUtility.MessageType.MethodeEnd);
                return Ok("success");
            }
            catch (Exception ex)
            {
                Log.Write(logFilePath, ex.Message, LogUtility.MessageType.Exception);
                return InternalServerError();
            }
        }

        public bool InsertToken(ExternalUserDto externalUserDto)
        {
            Log.Write(logFilePath, "InsertToken", LogUtility.MessageType.MethodeStart);
            try
            {
                TokenRepository tokenRepository = new TokenRepository();
                tokenRepository.DeleteTokenByUserName(externalUserDto.UserName);
                tokenRepository.InsertToken(externalUserDto.UserName, externalUserDto.AccessToken);
                Log.Write(logFilePath, "InsertToken", LogUtility.MessageType.MethodeEnd);
                return true;
            }
            catch (Exception ex)
            {
                Log.Write(logFilePath, ex.Message, LogUtility.MessageType.Exception);
                return false;
            }
        }
        [HttpPost]
        [Route("refresh")]
        public IHttpActionResult Post([FromBody] TokenDto dto)
        {
            try
            {
                Log.Write(logFilePath, "Refresh", LogUtility.MessageType.MethodeStart);
                var tokenContent = dto.TokenContent;
                Log.Write(logFilePath, "tokenContent : "+ tokenContent, LogUtility.MessageType.UserMessage);
                TokenRepository tokenRepository = new TokenRepository();
                // this status gets the value whether the token is expired or refreshed
                Log.Write(logFilePath, "UpdateTolen", LogUtility.MessageType.MethodeStart);
                string tokenRefeshStatus = tokenRepository.UpdateToken(tokenContent);
                Log.Write(logFilePath, "UpdateTolen", LogUtility.MessageType.MethodeEnd);
                if (tokenRefeshStatus.Equals("expired"))
                {
                    // initializing logout functinality
                    Log.Write(logFilePath, "DeleteTolen", LogUtility.MessageType.MethodeStart);
                    tokenRepository.DeleteToken(tokenContent);
                    Log.Write(logFilePath, "DeleteTolen", LogUtility.MessageType.MethodeEnd);
                }
                Log.Write(logFilePath, "Refresh", LogUtility.MessageType.MethodeEnd);
                return Ok(tokenRefeshStatus);

            }
            catch (Exception ex)
            {
                Log.Write(logFilePath, ex.Message, LogUtility.MessageType.Exception);
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("roles")]
        public IHttpActionResult GetRolesByToken(string tokenContent)
        {
            try
            {
                Log.Write(logFilePath, "roles", LogUtility.MessageType.MethodeStart);
                Log.Write(logFilePath, "tokenContent: "+ tokenContent, LogUtility.MessageType.UserMessage);
                TokenRepository tokenRepository = new TokenRepository();
                string userName = tokenRepository.GetUserNameByToken(tokenContent);
                RoleRepository roleRepository = new RoleRepository();
                var roles = roleRepository.GetRolesByUserName(userName);
                Log.Write(logFilePath, "roles", LogUtility.MessageType.MethodeEnd);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                Log.Write(logFilePath, ex.Message, LogUtility.MessageType.Exception);
                return InternalServerError();
            }
        }
        [HttpGet]
        [Route("user")]
        public IHttpActionResult GetUserByToken(string tokenContent)
        {
            try
            {
                Log.Write(logFilePath, "User", LogUtility.MessageType.MethodeStart);
                Log.Write(logFilePath, "tokenContent: " + tokenContent, LogUtility.MessageType.UserMessage);
                TokenRepository tokenRepository = new TokenRepository();
                string userName = tokenRepository.GetUserNameByToken(tokenContent);
                UserRepository userRepository = new UserRepository();
                var user = userRepository.GetByUserName(userName);
                Log.Write(logFilePath, "User", LogUtility.MessageType.MethodeEnd);
                return Json(user);
            }
            catch (Exception ex)
            {
                Log.Write(logFilePath, ex.Message, LogUtility.MessageType.Exception);
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("externalToken")]
        public IHttpActionResult GetExternalToken(string userName)
        {
            try
            {
                string token = JWTToken.GenerateToken(userName);
                return Ok(token);
            }
            catch (Exception e)
            {

                return InternalServerError(e);
            }
        }
    }
}
