using System;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace LogService
{
    public class Log
    {
        public static string DestinationPath { get; set; }
        public static string User { get; set; }
        ///<summary>
        ///<para>It will write your message to A default location.</para>
        ///<para>Default location may be on C:\\YeasinPublished\\ADA.txt</para>
        ///<para>Default prefix is Exception: </para>
        ///</summary>
        //public static bool Write(string message)
        //{
        //    return Write(message, LogUtility.MessageType.Exception);
        //}
        //public static bool Write(string message, LogUtility.MessageType prefix)
        //{
        //    if (!String.IsNullOrWhiteSpace(DestinationPath))
        //    {
        //        return Write(DestinationPath, message, prefix);
        //    }
        //    return Write(LogConfig.DestinationPath, message, prefix);
        //}
        public static bool Write(string path, string message)
        {
            return Write(path, message, LogUtility.MessageType.Exception);
        }
        public static bool Write(string path, string message, LogUtility.MessageType prefix)
        {
            WriteAsyncCaller(path,message,prefix);
            return true;
        }
        public  static void  Read(string path)
        {
            lock (LogConfig.GetLocker())
            {
                File.ReadAllText(path);
            }
        }

        private static void WriteAsyncCaller(string path, string message, LogUtility.MessageType prefix)
        {
            LogUtility.AsyncMethodCaller caller = WriteAsync;
            caller.BeginInvoke(path, message, prefix, AsyncCallback, null);
        }
        
        private static void WriteAsync(string path, string message, LogUtility.MessageType prefix)
        {
            message = DateTime.Now.ToShortDateString() +" "+ DateTime.Now.ToShortTimeString() + " " +(User ?? "Anymonus") + " " + LogUtility.GetString(prefix) + message;
            if (LogConfig.BuildMode.Equals(LogUtility.BuildMode.Debug))
            {
                try
                {
                    lock (LogConfig.GetLocker())
                    {
                        if (!File.Exists(path))
                        {
                            // Create a file to write to.
                            using (StreamWriter sw = File.CreateText(path))
                            {
                                sw.WriteLine(message);
                            }
                        }
                        // append a file with existing.
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine(message);
                        }

                    }

                }
                catch (Exception exception)
                {
                    lock (LogConfig.GetLocker())
                    {
                        File.WriteAllText(path, exception.Message);
                    }
                }
            }
        }
        
        private static void AsyncCallback(IAsyncResult ar)
        {
            try
            {
                AsyncResult result = (AsyncResult)ar;
                LogUtility.AsyncMethodCaller caller = (LogUtility.AsyncMethodCaller)result.AsyncDelegate;
                caller.EndInvoke(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: "+e.InnerException);
                //Todo: 
            }
        }
    }

    public class Lof
    {
        public static string EventLogPath = "C:/YeasinPublished/adaEvents.txt";
        public static string ErrorLogPath = "C:/YeasinPublished/adaErrors.txt";
        private static readonly object Lock = new object();
        private static Lof _instance;

        public static Lof Instance
        {
            get
            {
                lock (Lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Lof();
                    }
                    return _instance;
                }
            }
        }

        public string DestinationPath { get; set; }
        public string User { get; set; }
        ///<summary>
        ///<para>It will write your message to A default location.</para>
        ///<para>Default location may be on C:\\YeasinPublished\\ADA.txt</para>
        ///<para>Default prefix is Exception: </para>
        ///</summary>
        //public static bool Write(string message)
        //{
        //    return Write(message, LogUtility.MessageType.Exception);
        //}
        //public static bool Write(string message, LogUtility.MessageType prefix)
        //{
        //    if (!String.IsNullOrWhiteSpace(DestinationPath))
        //    {
        //        return Write(DestinationPath, message, prefix);
        //    }
        //    return Write(LogConfig.DestinationPath, message, prefix);
        //}
        public bool Write(string path, string message)
        {
            return Write(path, message, LogUtility.MessageType.Exception);
        }
        public bool Write(string path, string message, LogUtility.MessageType prefix)
        {
            WriteAsyncCaller(path, message, prefix);
            return true;
        }
        
        public void Read(string path)
        {
            lock (LogConfig.GetLocker())
            {
                File.ReadAllText(path);
            }
        }

        private void WriteAsyncCaller(string path, string message, LogUtility.MessageType prefix)
        {
            LogUtility.AsyncMethodCaller caller = WriteAsync;
            caller.BeginInvoke(path, message, prefix, AsyncCallback, null);
        }

        private void WriteAsync(string path, string message, LogUtility.MessageType prefix)
        {

            message = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " " + (User != null? User : "anymonus") + " " + LogUtility.GetString(prefix) + message;
            if (LogConfig.BuildMode.Equals(LogUtility.BuildMode.Debug))
            {
                try
                {
                    lock (LogConfig.GetLocker())
                    {
                        if (!File.Exists(path))
                        {
                            // Create a file to write to.
                            using (StreamWriter sw = File.CreateText(path))
                            {
                                sw.WriteLine(message);
                            }
                        }
                        // append a file with existing.
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine(message);
                        }

                    }

                }
                catch (Exception exception)
                {
                    lock (LogConfig.GetLocker())
                    {
                        File.WriteAllText(path, exception.Message);
                    }
                }
            }
        }

        private void AsyncCallback(IAsyncResult ar)
        {
            try
            {
                AsyncResult result = (AsyncResult)ar;
                LogUtility.AsyncMethodCaller caller = (LogUtility.AsyncMethodCaller)result.AsyncDelegate;
                caller.EndInvoke(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.InnerException);
                //Todo: 
            }
        }



        public bool Event(string path, RequestMetaData requestMetaData, LogUtility.MessageType prefix)
        {
            EventAsyncCaller(path, requestMetaData, prefix);
            return true;
        }

        private void EventAsyncCaller(string path, RequestMetaData requestMetaData, LogUtility.MessageType prefix)
        {
            LogUtility.AsyncEventMethodCaller caller = EventAsync;
            caller.BeginInvoke(path, requestMetaData, prefix, AsyncEventCallback, null);
        }

        private void EventAsync(string path, RequestMetaData requestMetaData, LogUtility.MessageType prefix)
        {
            var message = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " " + LogUtility.GetString(prefix) + " " + requestMetaData.BrowserName + " " + requestMetaData.BrowserVersion + " " + requestMetaData.MachineName + " " + requestMetaData.MachineUser + " " + requestMetaData.OperatingSystem + " " + requestMetaData.IpAddress + " " + requestMetaData.UrlScheme + " " + requestMetaData.UrlHost + " " + requestMetaData.UrlPort + " " + requestMetaData.UrlQueryString + " " + requestMetaData.UrlSegments[2].Remove(requestMetaData.UrlSegments[2].Length-1) + " ";
            if (LogConfig.BuildMode.Equals(LogUtility.BuildMode.Debug))
            {
                try
                {
                    lock (LogConfig.GetLocker())
                    {
                        if (!File.Exists(path))
                        {
                            // Create a file to write to.
                            using (StreamWriter sw = File.CreateText(path))
                            {
                                sw.WriteLine(message);
                            }
                        }
                        // append a file with existing.
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine(message);
                        }

                    }

                }
                catch (Exception exception)
                {
                    lock (LogConfig.GetLocker())
                    {
                        File.WriteAllText(path, exception.Message);
                    }
                }
            }
        }

        private void AsyncEventCallback(IAsyncResult ar)
        {
            try
            {
                AsyncResult result = (AsyncResult)ar;
                LogUtility.AsyncEventMethodCaller caller = (LogUtility.AsyncEventMethodCaller)result.AsyncDelegate;
                caller.EndInvoke(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.InnerException);
                //Todo: 
            }
        }




        public bool Error(string path, RequestMetaData requestMetaData, ErrorMetaData errorMetaData, LogUtility.MessageType prefix)
        {
            ErrorAsyncCaller(path, requestMetaData, errorMetaData, prefix);
            return true;
        }

        private void ErrorAsyncCaller(string path, RequestMetaData requestMetaData, ErrorMetaData errorMetaData, LogUtility.MessageType prefix)
        {
            LogUtility.AsyncErrorMethodCaller caller = ErrorAsync;
            caller.BeginInvoke(path, requestMetaData, errorMetaData, prefix, AsyncErrorCallback, null);
        }

        private void ErrorAsync(string path, RequestMetaData requestMetaData, ErrorMetaData errorMetaData, LogUtility.MessageType prefix)
        {
            var message = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " " + LogUtility.GetString(prefix) + " " + requestMetaData.BrowserName + " " + requestMetaData.BrowserVersion + " " + requestMetaData.MachineName + " " + requestMetaData.MachineUser + " " + requestMetaData.OperatingSystem + " " + requestMetaData.IpAddress + " " + requestMetaData.UrlScheme + " " + requestMetaData.UrlHost + " " + requestMetaData.UrlPort + " " + requestMetaData.UrlQueryString + " " + requestMetaData.UrlSegments[2].Remove(requestMetaData.UrlSegments[2].Length - 1) + " "+errorMetaData.ErrorMessage;
            if (LogConfig.BuildMode.Equals(LogUtility.BuildMode.Debug))
            {
                try
                {
                    lock (LogConfig.GetLocker())
                    {
                        if (!File.Exists(path))
                        {
                            // Create a file to write to.
                            using (StreamWriter sw = File.CreateText(path))
                            {
                                sw.WriteLine(message);
                            }
                        }
                        // append a file with existing.
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine(message);
                        }

                    }

                }
                catch (Exception exception)
                {
                    lock (LogConfig.GetLocker())
                    {
                        File.WriteAllText(path, exception.Message);
                    }
                }
            }
        }

        private void AsyncErrorCallback(IAsyncResult ar)
        {
            try
            {
                AsyncResult result = (AsyncResult)ar;
                LogUtility.AsyncErrorMethodCaller caller = (LogUtility.AsyncErrorMethodCaller)result.AsyncDelegate;
                caller.EndInvoke(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.InnerException);
                //Todo: 
            }
        }

        public RequestMetaData RequestMetaData { get; set; }
    }

    public class RequestMetaData
    {
        public string MachineName { get; set; }
        public string MachineUser { get; set; }
        public string OperatingSystem { get; set; }
        public string IpAddress { get; set; }
        public string BrowserName { get; set; }
        public string BrowserVersion { get; set; }
        public string AbsulateUri { get; set; }
        public string UrlScheme { get; set; }
        public string UrlHost { get; set; }
        public string UrlPort { get; set; }
        public string UrlQueryString { get; set; }
        public string[] UrlSegments { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ErrorMetaData
    {
        public string ErrorMessage { get; set; }
        public string ErrorInnerMessage { get; set; }
        public string ErrorCode { get; set; }
    }
}
