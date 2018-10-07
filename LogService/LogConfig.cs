using System;

namespace LogService
{
    class LogConfig
    {
        public static LogUtility.BuildMode BuildMode = LogUtility.BuildMode.Debug;
        //public static string DestinationPath2 = "C:\\YeasinPublished\\ADA.txt";
        public static string DestinationPath = Environment.CurrentDirectory+"\\ADALog.txt";
        public static string EventLogPath = "C:/YeasinPublished/adaTest.txt";

        private static object _locker;

        public static object GetLocker()
        {
            if (_locker==null)
            {
                _locker = new object();

            }
            return _locker;
        }
    }
}
