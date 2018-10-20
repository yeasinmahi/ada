using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using Flogging.Web.Attributes;
using Flogging.Web.Services;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace AkijRest.IdentityServer.ApiFixed
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //
            config.SuppressDefaultHostAuthentication();
            
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Filters.Add(new AuthorizeAttribute());

            config.Filters.Add(new ApiLoggerAttribute("AkijRest"));

            config.Services.Replace(typeof(IExceptionHandler), new CustomApiExceptionHandler());
            config.Services.Add(typeof(IExceptionLogger), new CustomApiExceptionLogger("AkijRest"));

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);


            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //
            // Web API routes
            /*
            config.MapHttpAttributeRoutes();
            config.EnableCors();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            */
            var jsonFormatter =
                config.Formatters.OfType<JsonMediaTypeFormatter>().First();

            jsonFormatter.SerializerSettings.ContractResolver
                = new CamelCasePropertyNamesContractResolver();
            
            config.Services.Replace(typeof(IExceptionLogger), new UnhandledExceptionLogger());
            //config.MessageHandlers.Add(new CustomLogHandler());

        }
    }
}
