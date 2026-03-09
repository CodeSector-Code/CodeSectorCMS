using System;
using System.Net;
using System.Net.Mail;
using CodeSectorCMS.Domain;
using CodeSectorCMS.Domain.MessageModels;

namespace CodeSectorCMS.MessageService.Services
{
    public class MailService : IMailService
    {
        private readonly ILogger<MailService> logger;

        public MailService(ILogger<MailService> logger)
        {
            this.logger = logger;
        }

        public void SendMail(MailConfig mconfig, CreatedMessage message, string subscriberEmail)
        {
            // Setup message details
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(mconfig.Email);
            mailMessage.To.Add(subscriberEmail);

            mailMessage.Subject = message.Subject;
            mailMessage.Body = message.Body;

            // Setup SMTP client details
            using var smtpClient = new SmtpClient
            {
                Host = mconfig.SMTPServerAddr,
                Port = mconfig.Port,
                EnableSsl = mconfig.UseSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mailMessage.From.Address, "ktqt tldm rrul qvgy")
            };
            smtpClient.Send(mailMessage);

            logger.LogInformation("E-mail sent!");
        }
    }
}
