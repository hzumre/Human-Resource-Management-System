using IK_Project.Infrastructure.IMailSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IK_Project.Domain.Entities.Concrete;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace IK_Project.Application.Services.EmailSenderService
{
    public class EmailSenderService : IEmailSender
    {
        readonly IConfiguration _configuration;
        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;   
        }

        public async Task SendEmailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendEmailAsync(new[] { to }, subject, body, isBodyHtml);

        }

        public async Task SendEmailAsync(string[] toS, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in toS)
            {
                mail.To.Add(to);
            }

            mail.Body = body;
            mail.Subject = subject;
            mail.From = new MailAddress(_configuration["Mail:Username"], "TeamThreeHR", System.Text.Encoding.UTF8);

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.Port =Convert.ToInt32(_configuration["Mail:Port"]);
            smtp.EnableSsl = true;
            smtp.Host = _configuration["Mail:Host"];
            await smtp.SendMailAsync(mail);
        }
    }
}
