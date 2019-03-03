namespace OnlineLibraryManagementSystem.Services.Implementations
{
    using Microsoft.Extensions.Options;
    using OnlineLibraryManagementSystem.Models;
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            this.emailSettings = emailSettings.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var credentials = new NetworkCredential(this.emailSettings.Sender, this.emailSettings.Password);

                var mail = new MailMessage()
                {
                    From = new MailAddress(this.emailSettings.Sender, this.emailSettings.SenderName),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mail.To.Add(new MailAddress(email));

                var client = new SmtpClient()
                {
                    Port = this.emailSettings.MailPort,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = this.emailSettings.MailServer,
                    EnableSsl = true,
                    Credentials = credentials
                };

                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

            return Task.CompletedTask;
        }
    }
}