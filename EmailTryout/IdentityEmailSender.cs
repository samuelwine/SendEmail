using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace EmailTryout
{
    public class IdentityEmailSender : IEmailSender
    {
        private readonly EmailConfig _emailConfig;

        public IdentityEmailSender()
        {
            _emailConfig = new EmailConfig
            {
                From = "shmuelyaakovbluecare@gmail.com",
                SmtpServer = "smtp.gmail.com",
                Port = 587,
                Username = "shmuelyaakovbluecare@gmail.com",
                Password = "bhylfvhxqwyatndq"
            };
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new Message(new List<Recipient>
            {
                new Recipient
                {
                    Address = email,
                    Name = "You"
                }
            }, subject, htmlMessage);


            SendEmail(message);

            return Task.CompletedTask;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("SafeGuarding-App", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message.Content
            };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();

            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.Username, _emailConfig.Password);

                client.Send(mailMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }


    }
}
