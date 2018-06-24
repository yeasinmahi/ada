using System;
using System.Web.Http;
using System.Web.Http.Cors;
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
                // Getting the token from the identity server
                Log.Write(logFilePath, "CustomToken", LogUtility.MessageType.MethodeStart);
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

                Log.Write(content, LogUtility.MessageType.UserMessage);
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
                var tokenContent = dto.TokenContent;
                TokenRepository tokenRepository = new TokenRepository();
                // this status gets the value whether the token is expired or refreshed
                string tokenRefeshStatus = tokenRepository.UpdateToken(tokenContent);
                if (tokenRefeshStatus.Equals("expired"))
                {
                    // initializing logout functinality
                    tokenRepository.DeleteToken(tokenContent);
                }
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
                TokenRepository tokenRepository = new TokenRepository();
                string userName = tokenRepository.GetUsernameByToken(tokenContent);
                RoleRepository roleRepository = new RoleRepository();
                var roles = roleRepository.GetRolesByUserName(userName);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                Log.Write(logFilePath, ex.Message, LogUtility.MessageType.Exception);
                return InternalServerError();
            }
        }
    }
}
