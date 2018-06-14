using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Repositories;
using AkijRest.SolutionConstant;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AkijRest.IdentityServer.Controllers
{
    [EnableCors(origins: UrlConstant.WebClient, headers: "*", methods: "*")]
    [RoutePrefix("api/token")]
    public class TokenController : ApiController
    {
        [HttpPost]
        [Route("")]
        public IHttpActionResult CustomToken ([FromBody] UserDto userDto)
        {
            try
            {
                // Getting the token from the identity server

                var client = new RestClient(UrlConstant.IdentityServer);

                var request = new RestRequest("oauth/token", Method.POST);

                request.AddParameter("client_id", "099153c2625149bc8ecb3e85e03f0022");                
                request.AddParameter("grant_type", "password");
                request.AddParameter("username", userDto.UserName);
                request.AddParameter("password", userDto.Password);

                IRestResponse response = client.Execute(request);
                var content = response.Content;
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
                // the task ends

                return Ok(content);

            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }


        [HttpPost]
        [Route("external")]
        public IHttpActionResult ExternalToken([FromBody] ExternalUserDto externalUserDto)
        {
            try
            {
                InsertToken(externalUserDto);
                return Ok("success");
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }

        public bool InsertToken(ExternalUserDto externalUserDto)
        {
            try
            {
                TokenRepository tokenRepository = new TokenRepository();
                tokenRepository.DeleteTokenByUserName(externalUserDto.UserName);
                tokenRepository.InsertToken(externalUserDto.UserName, externalUserDto.AccessToken);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
            

        }
    }
}
