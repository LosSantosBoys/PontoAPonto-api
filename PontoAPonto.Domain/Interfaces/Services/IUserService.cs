using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Models;
using PontoAPonto.Domain.Models.Entities;

namespace PontoAPonto.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<CustomActionResult> AddUserAsync(User user);
        Task<CustomActionResult> ValidateDuplicateUserAsync(string email, string phone, string cpf);
        Task<CustomActionResult> DeleteUserByEmailAsync(string email);
        Task<bool> ValidateOtpAsync(ValidateOtpRequest request);
        Task<bool> GenerateNewOtpAsync(string email);
        Task<BaseResponse<SignInResponse>> SignInAsync(SignInRequest request);
        Task<bool> ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(string token, ResetPasswordRequest request);
    }
}
