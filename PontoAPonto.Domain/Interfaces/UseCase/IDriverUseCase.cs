using Microsoft.AspNetCore.Http;
using PontoAPonto.Domain.Dtos.Requests.Drivers;
using PontoAPonto.Domain.Dtos.Responses.Driver;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Domain.Interfaces.UseCase
{
    public interface IDriverUseCase
    {
        Task<CustomActionResult> CaptureValidationPictureAsync(CapturePictureRequest request, string? email);
        Task<CustomActionResult> CaptureDocumentPictureAsync(CapturePictureRequest request, string? email);
        Task<CustomActionResult> CaptureCarLicenseAsync(IFormFile pdfFile, string? email);
        Task<CustomActionResult<DriverProfileResponse>> GetDriverProfileAsync(string? email);
        Task<CustomActionResult> ChangeProfileAsync(ChangeProfileRequest request, string? email);
        Task<CustomActionResult> DeleteAccountAsync(string? email);
    }
}