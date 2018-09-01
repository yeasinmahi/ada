using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using LogService;

namespace AkijRest.IdentityServer.ApiFixed
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var exception = context.Exception;
            var httpException = exception as HttpException;
            if (httpException !=null)
            {
                context.Result = new CustomErrorResult(context.Request,
                    (HttpStatusCode)httpException.GetHttpCode(),
                    httpException.Message);
                return;
            }
            RequestMetaData requestMetaData = new RequestMetaData
            {
                ErrorMessage = context.Exception.Message
            };
            Lof.Instance.Event(Lof.ErrorLogPath, requestMetaData, LogUtility.MessageType.RequestEnd);
            // Return HttpStatusCode for other types of exception.

            context.Result = new CustomErrorResult(context.Request,
                HttpStatusCode.InternalServerError,
                exception.Message);
        }
    }
    public class CustomErrorResult : IHttpActionResult
    {
        private readonly string _errorMessage;
        private readonly HttpRequestMessage _requestMessage;
        private readonly HttpStatusCode _statusCode;

        public CustomErrorResult(HttpRequestMessage requestMessage,
            HttpStatusCode statusCode, string errorMessage)
        {
            _requestMessage = requestMessage;
            _statusCode = statusCode;
            _errorMessage = errorMessage;
        }

        public Task<HttpResponseMessage> ExecuteAsync(
            CancellationToken cancellationToken)
        {
            return Task.FromResult(_requestMessage.CreateErrorResponse(
                _statusCode, _errorMessage));
        }
    }

    public class UnhandledExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var log = context.Exception.Message;
            ErrorMetaData errorMetaData = new ErrorMetaData();
            errorMetaData.ErrorMessage = context.Exception.Message;
            //Write the exception to your logs
            RequestMetaData requestMetaData = Lof.Instance.RequestMetaData;
            Lof.Instance.Error(Lof.EventLogPath, requestMetaData, errorMetaData,  LogUtility.MessageType.Exception);
        }
    }
}