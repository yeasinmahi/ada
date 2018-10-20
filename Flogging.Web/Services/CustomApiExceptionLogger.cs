using Flogging.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace Flogging.Web.Services
{
    public class CustomApiExceptionLogger : ExceptionLogger
    {
        private string _productName;
        public CustomApiExceptionLogger(string productName)
        {
            _productName = productName;
        }
        public override void Log(ExceptionLoggerContext context)
        {
            var dict = new Dictionary<string, object>();

            string userId, userName;
            var user = context.RequestContext.Principal as ClaimsPrincipal;
            Helpers.GetUserData(dict, user, out userId, out userName);

            string location;
            Helpers.GetLocationForApiCall(context.RequestContext, dict, out location);

            var errorId = Guid.NewGuid().ToString();
            // This is here because the Logger is called BEFORE the Handler in the 
            //Web API exception pipeline   
            context.Exception.Data.Add("ErrorId", errorId);

            var logEntry = new FlogDetail()
            {
                CorrelationId = errorId,
                Product = _productName,
                Layer = "API",
                Location = location,
                Hostname = Environment.MachineName,
                Exception = context.Exception,
                UserId = userId,
                UserName = userName,
                AdditionalInfo = dict
            };

            Flogger.WriteError(logEntry);
        }
    }
}
