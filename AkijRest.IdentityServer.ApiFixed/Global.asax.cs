using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LogService;

namespace AkijRest.IdentityServer.ApiFixed
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var ipAddress = Request.UserHostAddress;
            var browser = Request.Browser.Browser;
            var browserVersion = Request.FilePath;
            var filePath = Request.UserHostAddress;
            var queryStrings = Request.QueryString.AllKeys;
            Lof.Instance.Write("C:/YeasinPublished/adaTest.txt", "Begin Request", LogUtility.MessageType.UserMessage);
        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {
            var response = Response;
            Lof.Instance.Write("C:/YeasinPublished/adaTest.txt", "End Request", LogUtility.MessageType.UserMessage);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}
