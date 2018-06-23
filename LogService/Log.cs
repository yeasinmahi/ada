using System;
using System.IO;

namespace LogService
{
    public static  class Log
    {
        public static string DestinationPath { get; set; }

        ///<summary>
        ///<para>It will write your message to A default location.</para>
        ///<para>Default location may be on C:/ADA.txt</para>
        ///<para>Default prefix is Exception: </para>
        ///</summary>
        public static bool Write(string message)
        {
            return Write(message, LogUtility.MessageType.Exception);
        }
        public static bool Write(string message, LogUtility.MessageType prefix)
        {
            if (!String.IsNullOrWhiteSpace(DestinationPath))
            {
                return Write(DestinationPath, message, prefix);
            }
            return Write(LogConfig.DestinationPath, message, prefix);
        }
        public static bool Write(string path, string message, LogUtility.MessageType prefix)
        {
            message = LogUtility.GetString(prefix) + message;
            if (LogConfig.BuildMode.Equals(LogUtility.BuildMode.Debug))
            {
                try
                {
                    if (File.Exists(path))
                    {
                        // append a file with existing.
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine(message);
                            return true;
                        }
                    }
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(message);
                    }
                    return true;
                }
                catch (Exception exception)
                {
                    File.WriteAllText(LogConfig.DestinationPath, exception.Message);
                    return false;
                }
            }
            return false;
        }
        public  static void  Read(string path)
        {
            File.ReadAllText(path);
        }
    }
}
