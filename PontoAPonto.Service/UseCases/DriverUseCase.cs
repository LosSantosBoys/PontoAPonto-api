using Microsoft.AspNetCore.Mvc;
using PontoAPonto.Domain.Dtos.Requests.Drivers;
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

        public async Task<CustomActionResult> CaptureProfilePicture([FromBody] CaptureProfilePictureRequest request, string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return DriverError.Unauthorized();
            }

            return await _driverService.CaptureProfilePicture(email, request.ImageBase64);
        }
    }
}
