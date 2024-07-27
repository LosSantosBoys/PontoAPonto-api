using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Domain.Interfaces.Services
{
    public interface ISignUpService
    {
        Task<CustomActionResult> CreateUserSignUpAsync(SignUpRequest request);
    }
}