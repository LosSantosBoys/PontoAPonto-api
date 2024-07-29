using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Domain.Interfaces.UseCase
{
    public interface ISignInUseCase
    {
        Task<CustomActionResult<SignInResponse>> SignInAsync(SignInRequest request);
    }
}
