using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Interfaces.Repositories;
using PontoAPonto.Domain.Models.Entities;

namespace PontoAPonto.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<BaseResponse<OtpUserResponse>> CreateUserOtpAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
