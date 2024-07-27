using PontoAPonto.Domain.Models;

namespace PontoAPonto.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task<CustomActionResult> SendEmailAsync(string destination, string subject, string message);
    }
}
