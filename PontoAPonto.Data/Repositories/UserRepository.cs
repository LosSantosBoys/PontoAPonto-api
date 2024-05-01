using Microsoft.EntityFrameworkCore;
using PontoAPonto.Data.Contexts;
using PontoAPonto.Domain.Interfaces.Repositories;
using PontoAPonto.Domain.Models.Entities;

namespace PontoAPonto.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _userContext;

        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                await _userContext.Users.AddAsync(user);
                return await _userContext.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                _userContext.Update(user);
                return await _userContext.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userContext.Users.FirstAsync(x => x.Email == email);
        }

        public async Task<User> GetUserByTokenAsync(string token)
        {
            return await _userContext.Users.FirstAsync(x => x.PasswordResetToken == token);
        }
    }
}
