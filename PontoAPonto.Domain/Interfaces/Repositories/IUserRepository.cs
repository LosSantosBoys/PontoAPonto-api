using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Models.Entities;

namespace PontoAPonto.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<BaseResponse<OtpUserResponse>> CreateUserOtpAsync(User user);
    }
}
