using PontoAPonto.Domain.Models;
using PontoAPonto.Domain.Models.Entities;

namespace PontoAPonto.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<CustomActionResult> AddUserAsync(User user);
        Task<CustomActionResult> ValidateDuplicateUserAsync(string email, string phone, string cpf);
        Task<User> GetUserByEmailAsync(string email);
        Task<CustomActionResult> UpdateUserAsync(User user);
        Task<User> GetUserByTokenAsync(string token);
        Task<CustomActionResult> DeleteUserByEmailAsync(string email);
    }
}
