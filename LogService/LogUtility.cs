using System;

namespace LogService
{
    public class LogUtility
    {
        public enum BuildMode
        {
            Debug,
            Release
        }

        public enum MessageType
        {
            Exception,
            UserMessage,
            MethodeStart,
            MethodeEnd,
            RequestStart,
            RequestEnd
        }

        public static string GetString(MessageType prefix)
        {
            switch (prefix)
            {
                case MessageType.Exception:
                    return "Exception: ";
                case MessageType.UserMessage:
                    return "User: ";
                case MessageType.MethodeStart:
                    return "Method Start: ";
                case MessageType.MethodeEnd:
                    return "Method End: ";
                case MessageType.RequestStart:
                    return "Request Start: ";
                case MessageType.RequestEnd:
                    return "Request End: ";

            }
            return String.Empty;
        }
        public delegate void AsyncMethodCaller(string path, string message, MessageType prefix);
        public delegate void AsyncEventMethodCaller(string path, RequestMetaData requestMetaData, MessageType prefix);
        public delegate void AsyncErrorMethodCaller(string path, RequestMetaData requestMetaData, ErrorMetaData errorMetaData, MessageType prefix);
    }
}
