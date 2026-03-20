using System;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using CodeSectorCMS.Domain;
using CodeSectorCMS.Domain.MessageModels;

namespace CodeSectorCMS.MessageService.Services
{
    public class MailService : IMailService
    {
        private readonly ILogger<MailService> logger;
        private readonly IConfiguration configuration;

        public MailService(ILogger<MailService> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        public async void SendMail(MailConfig mconfig, CreatedMessage msg, string subscriberEmail)
        {
            // Wrap the client and message in using statements
            using (var client = new SmtpClient())
            using (var message = new MimeMessage())
            {
                message.From.Add(new MailboxAddress(mconfig.SenderName, mconfig.Email));
                message.To.Add(new MailboxAddress(null, subscriberEmail));
                message.Subject = msg.Subject;

                var bodyBuilder = new BodyBuilder
                {
                    TextBody = msg.Body,
                    HtmlBody = PixelHtmlBody(msg.MessageID, msg.Body)
                };
                message.Body = bodyBuilder.ToMessageBody();

                // Connect to the SMTP server
                await client.ConnectAsync(mconfig.SMTPServerAddr, mconfig.Port, mconfig.UseSSL ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(mconfig.Email, mconfig.PasswordKey); // Username might be "apikey" for some providers
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

            logger.LogInformation("E-mail sent!");
        }

        public string PixelHtmlBody(int id, string body)
        {
            var trackingUrl = $"{configuration["CodeSectorCMSMessageService"]}/api/response/track/?id={id}";

            return $@"
        <html>
        <body>
            <b>{body}</b>
            <!-- Tracking pixel embedded below -->
            <!--<img src=""{configuration["CodeSectorCMSMessageService"]}/images/test.png"" />-->
            <img src=""{trackingUrl}"" alt="""" width=""1"" height=""1"" style=""display:none;"" />
        </body>
        </html>";
        }
    }
}
