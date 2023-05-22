using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using MimeKit;
using MailKit.Net.Smtp;

namespace Url_RAP_checker.Module.Url
{
    public class SendEmailNotification
    {
        public async Task<bool> SendEmail(string email, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sender Name", "notification.broke.link@gmail.com"));
            message.To.Add(new MailboxAddress("Recipient Name", email));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
            };
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("notification.broke.link@gmail.com", "jbvenapvbsayhpfy");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception e)
            {
                string ss = e.Message;
                throw;
            }
            
            return true;
        }        
    }
}
