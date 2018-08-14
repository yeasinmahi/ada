using System;
using System.Net;
using System.Net.Mail;
using System.Runtime.Remoting.Messaging;
using LogService;

namespace EmailService
{
    public class Email
    {
        static string logFilePath = "C:/YeasinPublished/ada.txt";
        private static void SendEmail(EmailOptions emailOptions)
        {
            try
            {
                Log.Write(logFilePath, "Email", LogUtility.MessageType.MethodeStart);
                MailAddress fromAddress = new MailAddress(EmailConstant.EmailFromAddress, EmailConstant.EmailFromDisplayName);
                MailAddress toAddress = new MailAddress(emailOptions.ToAddress, emailOptions.ToAddressDisplayName);
                string fromPassword = EmailConstant.EmailFromAddressPassword;
                string subject = emailOptions.Subject;
                string body = emailOptions.Body;

                SmtpClient smtpClient = GetSmtpClient(fromAddress.Address, fromPassword,Provider.Akij);
                if (smtpClient != null)
                {
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    })

                    {
                        Log.Write(logFilePath, "Email Sent", LogUtility.MessageType.MethodeStart);
                        ServicePointManager.ServerCertificateValidationCallback =
                            (s, certificate, chain, sslPolicyErrors) => true;
                        smtpClient.Send(message);
                        Log.Write(logFilePath, "Email Sent", LogUtility.MessageType.MethodeEnd);
                    }
                }
                else
                {
                    //todo
                }
                
            }
            catch (Exception e)
            {
                Log.Write(logFilePath,e.Message, LogUtility.MessageType.Exception);

                if (e.InnerException != null)
                {
                    Log.Write(logFilePath, e.InnerException.ToString(), LogUtility.MessageType.Exception);
                }

                //todo
            }
        }
        private static SmtpClient GetSmtpClient(string email, string password, Provider provider)
        {
            switch (provider)
            {
                case Provider.Gmail:
                    return GetSmtpForGmail(email, password);
                case Provider.Yahoo:
                    return GetSmtpForYahoo(email, password);
                case Provider.HotMail:
                    return GetSmtpForHotmail(email, password);
                case Provider.Akij:
                    return GetSmtpForAkij(email, password);
                default:
                    return null;
            }
            
        }

        private static SmtpClient GetSmtpForGmail(string email, string password)
        {
            return new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(email, password)
            };
        }
        private static SmtpClient GetSmtpForYahoo(string email, string password)
        {
            //Todo: Prepare For Yahoo
            return null;
        }
        private static SmtpClient GetSmtpForHotmail(string email, string password)
        {
            //Todo: Prepare For hotmail
            return null;
        }
        private static SmtpClient GetSmtpForAkij(string email, string password)
        {
            return new SmtpClient
            {
                Host = "ex.akij.net",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(email, password)
            };
        }
        public enum Provider
        {
            Gmail,
            Yahoo,
            HotMail,
            Akij
        }
        public static void Send(EmailOptions emailOptions)
        {
            AsyncMethodCaller caller = new AsyncMethodCaller(SendMailInSeperateThread);
            AsyncCallback callbackHandler = new AsyncCallback(AsyncCallback);
            caller.BeginInvoke(emailOptions, callbackHandler, null);
        }
        private delegate void AsyncMethodCaller(EmailOptions emailOptions);
        private static void SendMailInSeperateThread(EmailOptions emailOptions)
        {
            SendEmail(emailOptions);
        }
        private static void AsyncCallback(IAsyncResult ar)
        {
            try
            {
                AsyncResult result = (AsyncResult)ar;
                AsyncMethodCaller caller = (AsyncMethodCaller)result.AsyncDelegate;
                caller.EndInvoke(ar);
            }
            catch (Exception e)
            {
                //Todo: 
            }
        }
    }
}
