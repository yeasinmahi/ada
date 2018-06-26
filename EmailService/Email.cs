using System;
using System.Net;
using System.Net.Mail;
using System.Runtime.Remoting.Messaging;

namespace EmailService
{
    public class Email
    {
        private static void SendEmail(EmailOptions emailOptions)
        {
            try
            {
                MailAddress fromAddress = new MailAddress(EmailConstant.EmailFromAddress, EmailConstant.EmailFromDisplayName);
                MailAddress toAddress = new MailAddress(emailOptions.ToAddress, emailOptions.ToAddressDisplayName);
                string fromPassword = EmailConstant.EmailFromAddressPassword;
                string subject = emailOptions.Subject;
                string body = emailOptions.Body;

                SmtpClient smtpClient = GetSmtpClient(fromAddress.Address, fromPassword,Provider.Gmail);
                if (smtpClient != null)
                {
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtpClient.Send(message);
                    }
                }
                else
                {
                    //todo
                }
                
            }
            catch (Exception e)
            {
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
        public enum Provider
        {
            Gmail,
            Yahoo,
            HotMail
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
