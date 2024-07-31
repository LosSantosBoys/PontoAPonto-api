using PontoAPonto.Domain.Dtos.Requests.SignUp;
using PontoAPonto.Domain.Enums;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Domain.Interfaces.UseCase
{
    public interface ISignUpUseCase
    {
        Task<CustomActionResult> CreateSignUpAsync(SignUpRequest request, UserType userType);
        Task<CustomActionResult> ValidateOtpAsync(ValidateOtpRequest request);
        Task<CustomActionResult> CreateNewOtpAsync(CreateNewOtpRequest request);
    }
}
