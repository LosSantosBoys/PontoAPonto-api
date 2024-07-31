using PontoAPonto.Domain.Dtos.Requests.Drivers;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Domain.Interfaces.UseCase
{
    public interface IDriverUseCase
    {
        Task<CustomActionResult> CaptureProfilePictureAsync(CapturePictureRequest request, string? email);
        Task<CustomActionResult> CaptureDocumentPictureAsync(CapturePictureRequest request, string? email);
    }
}