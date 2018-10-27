using Serilog;
using Serilog.Events;

namespace Flogging.Core
{
    public static class Flogger
    {
        private static readonly ILogger _perfLogger;
        private static readonly ILogger _usageLogger;
        private static readonly ILogger _errorLogger;
        private static readonly ILogger _diagnosticLogger;

        static Flogger()
        {
            _perfLogger = new LoggerConfiguration()
                .WriteTo.File(path: @"c:\LogFiles\perf.txt")
                .WriteTo.Seq("http://agvdi4.akij.net:5341")
                .CreateLogger();

            _usageLogger = new LoggerConfiguration()
                .WriteTo.File(path: @"c:\LogFiles\usage.txt")
                .WriteTo.Seq("http://agvdi4.akij.net:5341")
                .CreateLogger();

            _errorLogger = new LoggerConfiguration()
                .WriteTo.File(path: @"c:\LogFiles\error.txt")
                .WriteTo.Seq("http://agvdi4.akij.net:5341")
                .CreateLogger();

            _diagnosticLogger = new LoggerConfiguration()
                .WriteTo.File(path: @"c:\LogFiles\diagnostic.txt")
                .WriteTo.Seq("http://agvdi4.akij.net:5341")
                .CreateLogger();
        }

        public static void WritePerf(FlogDetail infoToLog)
        {
            _perfLogger.Write(LogEventLevel.Information, "{@FlogDetail}", infoToLog);
        }

        public static void WriteUsage(FlogDetail infoToLog)
        {
            _usageLogger.Write(LogEventLevel.Information, "{@FlogDetail}", infoToLog);
        }

        public static void WriteError(FlogDetail infoToLog)
        {
            _errorLogger.Write(LogEventLevel.Error, "{@FlogDetail}", infoToLog);
        }

        public static void WriteDiagnostic(FlogDetail infoToLog)
        {
            //var writeDiagnostics = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableDiagnostics"]);

            /*
            if (!writeDiagnostics)
            {
                return;
            }
            */

            _diagnosticLogger.Write(LogEventLevel.Information, "{@FlogDetail}", infoToLog);
        }
    }
}
