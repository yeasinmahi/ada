using Flogging.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FloggingApi.Controllers
{
    public class DiagnosticController: ApiController
    {
        public void Write([FromBody] FlogDetail logEntry)
        {
            Flogger.WriteDiagnostic(logEntry);
        }
    }
}