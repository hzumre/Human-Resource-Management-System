using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IK_Project.Infrastructure.IMailSender
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body, bool isBodyHtml = true);
        Task SendEmailAsync(string[] toS, string subject, string body, bool isBodyHtml = true);
    }
}
