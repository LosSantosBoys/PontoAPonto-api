using PontoAPonto.Domain.Dtos.Requests.SignUp;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Domain.Interfaces.Services
{
    public interface ISignUpService
    {
        Task<CustomActionResult> CreateUserSignUpAsync(SignUpRequest request);
        Task<CustomActionResult> CreateDriverSignUpAsync(SignUpRequest request);
    }
}