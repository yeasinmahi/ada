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
    }
}
