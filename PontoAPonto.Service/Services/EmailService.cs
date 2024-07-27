using PontoAPonto.Domain.Errors;
using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Domain.Models;
using PontoAPonto.Domain.Models.Configs;
using System.Net;
using System.Net.Mail;
using static PontoAPonto.Domain.Constant.Constants.Email;

namespace PontoAPonto.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfig _email;

        public EmailService(EmailConfig email)
        {
            _email = email;
        }

        public async Task<CustomActionResult> SendEmailAsync(string destination, string subject, string message)
        {
            try
            {
                var client = new SmtpClient(SmtpServer.Host, SmtpServer.Port)
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

                return CustomActionResult.NoContent();
            }
            catch
            {
                return EmailError.SendEmailError();
            }
        }
    }
}
