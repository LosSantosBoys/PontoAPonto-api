using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Dtos.Requests.SignUp;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Domain.Interfaces.Services
{
    public interface ISignUpService
    {
        Task<CustomActionResult> CreateUserSignUpAsync(SignUpRequest request);
        Task<CustomActionResult> CreateDriverSignUpAsync(SignUpRequest request);
        Task<CustomActionResult> ValidateDriverOtpAsync(ValidateOtpRequest request);
        Task<CustomActionResult> ValidateUserOtpAsync(ValidateOtpRequest request);
        Task<CustomActionResult> CreateNewUserOtpAsync(string email);
        Task<CustomActionResult> CreateNewDriverOtpAsync(string email);
    }
}