using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Domain.Models.Configs;
using System.Net;
using System.Net.Mail;

namespace PontoAPonto.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfig _email;

        public EmailService(EmailConfig email)
        {
            _email = email;
        }

        public async Task SendEmailAsync(string destination, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(_email.Email, _email.Password)
            };

            var ms = new MailMessage(from: _email.Email,
                               to: destination,
                               subject,
                               message);

            ms.IsBodyHtml = true;

            await client.SendMailAsync(ms);
        }
    }
}
