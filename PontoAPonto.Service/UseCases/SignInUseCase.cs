using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Enums;
using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Domain.Interfaces.UseCase;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Service.UseCases
{
    public class SignInUseCase : ISignInUseCase
    {
        private readonly ISignInService _signInService;

        public SignInUseCase(ISignInService signInService)
        {
            _signInService = signInService;
        }

        public async Task<CustomActionResult<SignInResponse>> SignInAsync(SignInRequest request)
        {
            if (request.UserType == UserType.USER)
            {
                return await _signInService.SignInAsync(request);
            }

            return await _signInService.SignInDriverAsync(request);
        }
    }
}
