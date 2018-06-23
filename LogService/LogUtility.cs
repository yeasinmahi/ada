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
            MethodeEnd
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

            }
            return String.Empty;
        }
    }
}
