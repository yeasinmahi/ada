using System;

namespace LogService
{
    class LogConfig
    {
        public static LogUtility.BuildMode BuildMode = LogUtility.BuildMode.Release;
        //public static string DestinationPath2 = "C:\\YeasinPublished\\ADA.txt";
        public static string DestinationPath = Environment.CurrentDirectory+"\\ADALog.txt";
        public static object Locker= new object();
    }
}
