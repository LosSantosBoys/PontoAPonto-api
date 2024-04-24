using Microsoft.EntityFrameworkCore;
using PontoAPonto.Data.Contexts;
using PontoAPonto.Domain.Dtos.Responses;
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

        public async Task<bool> CreateUserOtpAsync(User user)
        {
            var response = new BaseResponse<OtpUserResponse>();

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
    }
}
