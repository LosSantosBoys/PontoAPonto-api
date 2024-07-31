using Microsoft.AspNetCore.Mvc;
using PontoAPonto.Domain.Dtos.Requests.Drivers;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Domain.Interfaces.UseCase
{
    public interface IDriverUseCase
    {
        Task<CustomActionResult> CaptureProfilePicture([FromBody] CaptureProfilePictureRequest request, string? email);
    }
}