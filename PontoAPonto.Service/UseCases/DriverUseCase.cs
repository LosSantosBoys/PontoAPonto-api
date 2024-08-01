using Microsoft.AspNetCore.Mvc;
using PontoAPonto.Domain.Dtos.Requests.Drivers;
using PontoAPonto.Domain.Dtos.Responses.Driver;
using PontoAPonto.Domain.Errors.Business;
using PontoAPonto.Domain.Interfaces.Repositories;
using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Domain.Interfaces.UseCase;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Service.UseCases
{
    public class DriverUseCase : IDriverUseCase
    {
        private readonly IDriverService _driverService;

        public DriverUseCase(IDriverService driverService, IS3Repository s3Repository)
        {
            _driverService = driverService;
        }

        public async Task<CustomActionResult> CaptureProfilePictureAsync([FromBody] CapturePictureRequest request, string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return DriverError.Unauthorized();
            }

            return await _driverService.CaptureFaceValidationPictureAsync(email, request.ImageBase64);
        }

        public async Task<CustomActionResult> CaptureDocumentPictureAsync(CapturePictureRequest request, string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return DriverError.Unauthorized();
            }

            return await _driverService.CaptureDocumentPictureAsync(email, request.ImageBase64);
        }

        public async Task<CustomActionResult> InsertCarInfoAsync(CarInfo request, string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return DriverError.Unauthorized();
            }

            return await _driverService.InsertCarInfoAsync(request, email);
        }

        public async Task<CustomActionResult<DriverProfileResponse>> GetDriverProfileAsync(string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return DriverError.Unauthorized();
            }

            return await _driverService.GetDriverProfileAsync(email);
        }

        public async Task<CustomActionResult> ChangeProfileAsync(ChangeProfileRequest request, string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return DriverError.Unauthorized();
            }

            return await _driverService.ChangeProfileAsync(request, email);
        }
    }
}
