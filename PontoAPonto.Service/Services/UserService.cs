using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Interfaces.Services;

namespace PontoAPonto.Service.Services
{
    public class UserService : IUserService
    {
        public UserService()
        {
         
        }

        public async Task<BaseResponse<OtpUserResponse>> CreateUserOtpAsync(OtpUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
