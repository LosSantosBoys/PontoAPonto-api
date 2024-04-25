using PontoAPonto.Domain.Models.Entities;

namespace PontoAPonto.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> AddUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> UpdateUserAsync(User user);
    }
}
