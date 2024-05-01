using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Dtos.Responses;

namespace PontoAPonto.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<BaseResponse<OtpUserResponse>> CreateUserSignUpAsync(SignUpRequest request);
        Task<bool> ValidateOtpAsync(ValidateOtpRequest request);
        Task<bool> GenerateNewOtpAsync(string email);
        Task<BaseResponse<SignInResponse>> SignInAsync(SignInRequest request);
        Task<bool> ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(string token, ResetPasswordRequest request);
    }
}
