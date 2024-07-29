using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Domain.Interfaces.Services
{
    public interface ISignInService
    {
        Task<CustomActionResult<SignInResponse>> SignInAsync(SignInRequest request);
        Task<CustomActionResult<SignInResponse>> SignInDriverAsync(SignInRequest request);
    }
}
