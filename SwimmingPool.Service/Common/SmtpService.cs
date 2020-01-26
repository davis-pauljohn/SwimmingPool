using Microsoft.Extensions.Configuration;
using SwimmingPool.Service.Common.EmailTemplates;
using System;
using System.Net;
using System.Net.Mail;

namespace SwimmingPool.Service.Common
{
    public class SmtpService : ISmtpService
    {
        private readonly IConfiguration configuration;

        public SmtpService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void SendEmail(string recipient, IEmailTemplate template)
        {
            var smtpSettings = configuration.GetSection("Smtp");
            var smtpClient = new SmtpClient(smtpSettings["Host"], int.Parse(smtpSettings["Port"]));

            smtpClient.Credentials = new NetworkCredential(smtpSettings["SystemEmail"], Environment.GetEnvironmentVariable("SwimmingPoolSmtpHostPassword", EnvironmentVariableTarget.Machine));
            smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            var mail = new MailMessage { From = new MailAddress(smtpSettings["SystemEmail"], smtpSettings["SystemSenderName"]), Body = template.GetTemplate() };
            mail.To.Add(new MailAddress(recipient));
            smtpClient.Send(mail);
        }
    }
}