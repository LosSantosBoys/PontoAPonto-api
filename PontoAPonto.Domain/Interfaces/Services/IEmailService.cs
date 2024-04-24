namespace PontoAPonto.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string destination, string subject, string message);
    }
}
