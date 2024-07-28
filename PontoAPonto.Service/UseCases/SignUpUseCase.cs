using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Dtos.Requests.SignUp;
using PontoAPonto.Domain.Enums;
using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Domain.Interfaces.UseCase;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Service.UseCases
{
    public class SignUpUseCase : ISignUpUseCase
    {
        private readonly ISignUpService _signUpService;

        public SignUpUseCase(ISignUpService signUpService)
        {
            _signUpService = signUpService;
        }

        public async Task<CustomActionResult> CreateSignUpAsync(SignUpRequest request, UserType userType)
        {
            if (userType == UserType.USER)
            {
                return await _signUpService.CreateUserSignUpAsync(request);
            }

            return await _signUpService.CreateDriverSignUpAsync(request);
        }

        public async Task<CustomActionResult> ValidateOtpAsync(ValidateOtpRequest request)
        {
            if (request.UserType == UserType.USER)
            {
                return await _signUpService.ValidateUserOtpAsync(request);
            }

            return await _signUpService.ValidateDriverOtpAsync(request);
        }

        public async Task<CustomActionResult> CreateNewOtpAsync(CreateNewOtpRequest request)
        {
            if (request.UserType == UserType.USER)
            {
                return await _signUpService.CreateNewUserOtpAsync(request.Email);
            }

            return await _signUpService.CreateNewDriverOtpAsync(request.Email);
        }
    }
}
