using Microsoft.EntityFrameworkCore;
using PontoAPonto.Data.Contexts;
using PontoAPonto.Domain.Errors;
using PontoAPonto.Domain.Interfaces.Repositories;
using PontoAPonto.Domain.Models;
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

        public async Task<CustomActionResult> ValidateDuplicateUserAsync(string email, string phone, string cpf)
        {
            var duplicateChecks = await _userContext.Users
                .Where(u => u.Email == email || u.Phone == phone || u.Cpf == cpf)
                .Select(u => new
                {
                    DuplicateEmail = u.Email == email,
                    DuplicatePhone = u.Phone == phone,
                    DuplicateCpf = u.Cpf == cpf
                })
            .FirstOrDefaultAsync();

            if (duplicateChecks != null)
            {
                return SignUpError.DataConflict(
                    duplicateChecks.DuplicateEmail, duplicateChecks.DuplicatePhone, duplicateChecks.DuplicateCpf);
            }

            return CustomActionResult.NoContent();
        }

        public async Task<CustomActionResult> AddUserAsync(User user)
        {
            try
            {
                await _userContext.Users.AddAsync(user);

                if (await _userContext.SaveChangesAsync() == 0)
                {
                    return SignUpError.DatabaseError();
                }

                return CustomActionResult.NoContent();
            }
            catch (Exception ex)
            {
                return SignUpError.DatabaseError();
            }
        }

        public async Task<CustomActionResult> UpdateUserAsync(User user)
        {
            try
            {
                _userContext.Update(user);

                if (await _userContext.SaveChangesAsync() == 0)
                {
                    return SignUpError.DatabaseError();
                }

                return CustomActionResult.NoContent();
            }
            catch (DbUpdateException ex)
            {
                return SignUpError.DatabaseError();
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

        public async Task<CustomActionResult> DeleteUserByEmailAsync(string email)
        {
            try
            {
                var rowsAffected = await _userContext.Users
                .Where(x => x.Email == email)
                .ExecuteDeleteAsync();

                if (rowsAffected == 0)
                {
                    return SignUpError.UserNotFound();
                }

                return CustomActionResult.NoContent();
            }
            catch (Exception ex)
            {
                return SignUpError.DatabaseError();
            }
        }
    }
}
