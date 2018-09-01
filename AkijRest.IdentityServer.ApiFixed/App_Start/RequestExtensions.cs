using System.Web;

namespace AkijRest.IdentityServer.ApiFixed
{
    public static class RequestExtensions
    {
        public static string GetIpAddress(this HttpRequest request)
        {
            if (request.Headers["CF-CONNECTING-IP"] != null)
                return request.Headers["CF-CONNECTING-IP"];

            var ipAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                var addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                    return addresses[0];
            }

            return request.UserHostAddress;
        }
    }
}