using System.Web;
using System.Web.Mvc;

namespace AkijRest.IdentityServer.ApiFixed
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
