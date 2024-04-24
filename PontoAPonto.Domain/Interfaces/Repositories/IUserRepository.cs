using PontoAPonto.Domain.Models.Entities;

namespace PontoAPonto.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> CreateUserOtpAsync(User user);
    }
}
