using Microsoft.Extensions.Configuration;
using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Domain.Models;
using System.Net;
using System.Net.Mail;

namespace PontoAPonto.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly EmailConfig _email;

        public EmailService(IConfiguration configuration, EmailConfig email)
        {
            _configuration = configuration;
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

            await client.SendMailAsync(
                new MailMessage(from: _email.Email,
                                to: destination,
                                subject,
                                message));
        }
    }
}
