using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AutoService.Workers
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var from = "servisavto605@gmail.com";
            var pass = "autoservice!1";
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(from, pass),
                EnableSsl = true
            };
            var mail = new MailMessage(from, email)
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            return client.SendMailAsync(mail);
        }
    }
}
