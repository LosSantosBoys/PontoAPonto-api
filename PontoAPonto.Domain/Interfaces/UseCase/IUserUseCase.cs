using PontoAPonto.Domain.Dtos.Responses.User;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Domain.Interfaces.UseCase
{
    public interface IUserUseCase
    {
        Task<CustomActionResult<UserProfileResponse>> GetUserProfileAsync(string? email);
    }
}
