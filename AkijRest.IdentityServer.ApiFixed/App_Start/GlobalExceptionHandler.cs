using System.Web.Http.ExceptionHandling;
using LogService;

namespace AkijRest.IdentityServer.ApiFixed
{
    public class UnhandledExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            ErrorMetaData errorMetaData = new ErrorMetaData {ErrorMessage = context.Exception.Message};
            //Write the exception to your logs
            RequestMetaData requestMetaData = LogService.Log.Instance.RequestMetaData;
            LogService.Log.Instance.Error(LogService.Log.EventLogPath, requestMetaData, errorMetaData,  LogUtility.MessageType.Exception);
        }
    }
}