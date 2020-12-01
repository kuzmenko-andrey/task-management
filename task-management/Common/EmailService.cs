using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace task_management.Common
{
    public class EmailService
    {
        private readonly string portalName;
        private readonly string portalMailAdress;

        private readonly string host;
        private readonly int port;
        private readonly bool useSsl;
        private readonly string password;

        public EmailService(IOptions<MailOptions> mailSettingsOption)
        {
            var mailSettings = mailSettingsOption.Value;
            portalName = mailSettings.SenderName;
            portalMailAdress = mailSettings.SenderAdress;
            host = mailSettings.ServerHost;
            port = mailSettings.ServerPort;
            useSsl = mailSettings.UseSsl;
            password = mailSettings.SenderPassword;
        }

        public void Send(Email email)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(host, port, useSsl);
                smtpClient.Authenticate(portalMailAdress, password);

                var message = CreateDefaultMessage(email);

                smtpClient.Send(message);
            }
        }

        public Task SendAsync(Email email, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(host, port, useSsl);
                smtpClient.Authenticate(portalMailAdress, password);

                var message = CreateDefaultMessage(email);

                return smtpClient.SendAsync(message, cancellationToken);
            }
        }

        private MimeMessage CreateDefaultMessage(Email email)
        {
            var message = new MimeMessage
            {
                From =
                {
                    new MailboxAddress(portalName, portalMailAdress)
                },
                To =
                {
                    new MailboxAddress(email.Recipient)
                },
                Subject = email.Subject,
                Body = new TextPart(TextFormat.Html)
                {
                    Text = email.Body
                }
            };

            return message;
        }

        public void Dispose()
        {
        }
    }
