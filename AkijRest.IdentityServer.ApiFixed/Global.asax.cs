using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AkijRest.IdentityServer.Repository.Repositories;
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
            
            RequestMetaData requestMetaData = new RequestMetaData
            {
                MachineName = Environment.MachineName,
                MachineUser = Environment.UserName,
                OperatingSystem = Environment.OSVersion.VersionString,
                IpAddress = Request.GetIpAddress(),
                BrowserName = Request.Browser.Browser,
                BrowserVersion = Request.Browser.Version,
                AbsulateUri = Request.Url.AbsoluteUri,
                UrlHost = Request.Url.Host,
                UrlScheme = Request.Url.Scheme,
                UrlPort = Request.Url.Port.ToString(),
                UrlQueryString = Request.Url.Query,
                UrlSegments = Request.Url.Segments
            };
            Log.Instance.RequestMetaData = requestMetaData;
            Log.Instance.Event(Log.EventLogPath, requestMetaData, LogUtility.MessageType.RequestStart);
        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {
            RequestMetaData requestMetaData = new RequestMetaData
            {
                MachineName = Environment.MachineName,
                MachineUser = Environment.UserName,
                OperatingSystem = Environment.OSVersion.VersionString,
                IpAddress = Request.GetIpAddress(),
                BrowserName = Request.Browser.Browser,
                BrowserVersion = Request.Browser.Version,
                AbsulateUri = Request.Url.AbsoluteUri,
                UrlHost = Request.Url.Host,
                UrlScheme = Request.Url.Scheme,
                UrlPort = Request.Url.Port.ToString(),
                UrlQueryString = Request.Url.Query,
                UrlSegments = Request.Url.Segments
            };
            Log.Instance.Event(Log.EventLogPath, requestMetaData, LogUtility.MessageType.RequestEnd);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}
