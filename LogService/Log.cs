using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using static LogService.LogUtility;

namespace LogService
{
    
    public class Log
    {
        public static string EventLogPath = "C:/YeasinPublished/Events.txt";
        public static string ErrorLogPath = "C:/YeasinPublished/Errors.txt";
        private static readonly object Lock = new object();
        private static Log _instance;

        public static Log Instance
        {
            get
            {
                lock (Lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Log();
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
            return Write(path, message, MessageType.Exception);
        }
        public bool Write(string path, string message, MessageType prefix)
        {
            //Error(path, RequestMetaData, new ErrorMetaData{ErrorMessage = message}, prefix);
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

        private void WriteAsyncCaller(string path, string message, MessageType prefix)
        {
            AsyncMethodCaller caller = WriteAsync;
            caller.BeginInvoke(path, message, prefix, AsyncCallback, null);
        }

        private void WriteAsync(string path, string message, MessageType prefix)
        {

            message = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " " + (User != null ? User : "anymonus") + " " + GetString(prefix) + message;
            if (LogConfig.BuildMode.Equals(BuildMode.Debug))
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
                AsyncMethodCaller caller = (AsyncMethodCaller)result.AsyncDelegate;
                caller.EndInvoke(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.InnerException);
                //Todo: 
            }
        }



        public bool Event(string path, RequestMetaData requestMetaData, MessageType prefix)
        {
            Error(path, requestMetaData, null, prefix);
            return true;
        }

        //private void EventAsyncCaller(string path, RequestMetaData requestMetaData, MessageType prefix)
        //{
        //    AsyncEventMethodCaller caller = EventAsync;
        //    caller.BeginInvoke(path, requestMetaData, prefix, AsyncEventCallback, null);
        //}

        //private void EventAsync(string path, RequestMetaData requestMetaData, MessageType prefix)
        //{
        //    var message = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " " + GetString(prefix) + " " + requestMetaData.BrowserName + " " + requestMetaData.BrowserVersion + " " + requestMetaData.MachineName + " " + requestMetaData.MachineUser + " " + requestMetaData.OperatingSystem + " " + requestMetaData.IpAddress + " " + requestMetaData.UrlScheme + " " + requestMetaData.UrlHost + " " + requestMetaData.UrlPort + " " + requestMetaData.UrlQueryString + " " + requestMetaData.UrlSegments[2].Remove(requestMetaData.UrlSegments[2].Length-1) + " ";
        //    if (LogConfig.BuildMode.Equals(BuildMode.Debug))
        //    {
        //        try
        //        {
        //            lock (LogConfig.GetLocker())
        //            {
        //                if (!File.Exists(path))
        //                {
        //                    // Create a file to write to.
        //                    using (StreamWriter sw = File.CreateText(path))
        //                    {
        //                        sw.WriteLine(message);
        //                    }
        //                }
        //                // append a file with existing.
        //                using (StreamWriter sw = File.AppendText(path))
        //                {
        //                    sw.WriteLine(message);
        //                }

        //            }

        //        }
        //        catch (Exception exception)
        //        {
        //            lock (LogConfig.GetLocker())
        //            {
        //                File.WriteAllText(path, exception.Message);
        //            }
        //        }
        //    }
        //}

        //private void AsyncEventCallback(IAsyncResult ar)
        //{
        //    try
        //    {
        //        AsyncResult result = (AsyncResult)ar;
        //        AsyncEventMethodCaller caller = (AsyncEventMethodCaller)result.AsyncDelegate;
        //        caller.EndInvoke(ar);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Exception: " + e.InnerException);
        //        //Todo: 
        //    }
        //}




        public bool Error(string path, RequestMetaData requestMetaData, ErrorMetaData errorMetaData, MessageType prefix)
        {
            ErrorAsyncCaller(path, requestMetaData, errorMetaData, prefix);
            return true;
        }

        private void ErrorAsyncCaller(string path, RequestMetaData requestMetaData, ErrorMetaData errorMetaData, MessageType prefix)
        {
            AsyncErrorMethodCaller caller = ErrorAsync;
            caller.BeginInvoke(path, requestMetaData, errorMetaData, prefix, AsyncErrorCallback, null);
        }

        private string RemoveLastIndex(string message)
        {
            return message.Remove(message.Length - 1);
        }
        private void ErrorAsync(string path, RequestMetaData requestMetaData, ErrorMetaData errorMetaData, MessageType prefix)
        {
            //var message = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " " + LogUtility.GetString(prefix) + " " + requestMetaData.BrowserName + " " + requestMetaData.BrowserVersion + " " + requestMetaData.MachineName + " " + requestMetaData.MachineUser + " " + requestMetaData.OperatingSystem + " " + requestMetaData.IpAddress + " " + requestMetaData.UrlScheme + " " + requestMetaData.UrlHost + " " + requestMetaData.UrlPort + " " + requestMetaData.UrlQueryString + " " + requestMetaData.UrlSegments[2].Remove(requestMetaData.UrlSegments[2].Length - 1) + " "+errorMetaData.ErrorMessage;
            var message = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12}",
                DateTime.Now.ToShortDateString(),
                DateTime.Now.ToShortTimeString(),
                GetString(prefix),
                requestMetaData.BrowserName,
                requestMetaData.BrowserVersion,
                requestMetaData.IpAddress,
                requestMetaData.UrlScheme,
                requestMetaData.UrlHost,
                requestMetaData.UrlPort,
                requestMetaData.UrlSegments.Length > 3 ? RemoveLastIndex(requestMetaData.UrlSegments[2]): requestMetaData.UrlSegments[2],
                requestMetaData.UrlSegments.Length > 3 ? requestMetaData.UrlSegments[3]:"get",
                requestMetaData.UrlQueryString,
                prefix.Equals(MessageType.Exception)? "\"" + errorMetaData.ErrorMessage + "\"":""
                );
            if (LogConfig.BuildMode.Equals(BuildMode.Debug))
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
                AsyncErrorMethodCaller caller = (AsyncErrorMethodCaller)result.AsyncDelegate;
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
