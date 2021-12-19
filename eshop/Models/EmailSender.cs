using eshop.Areas.Admin.Controllers;
using eshop.Models.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace eshop.Models
{
    public class EmailSender
    {
        public static void SendEmail(string subject, string body, string to)
        {
            var sender = new MailAddress("discgolfeshoputb@gmail.com", "Discgolf Eshop");
            var recipient = new MailAddress(to, "Dear Customer");
            const string senderPassword = "hoVno12345+";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(sender.Address, senderPassword)
            };

            var mail = new MailMessage(sender, recipient)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };
            
            smtp.Send(mail);
            smtp.Dispose();
        }

    }
}
