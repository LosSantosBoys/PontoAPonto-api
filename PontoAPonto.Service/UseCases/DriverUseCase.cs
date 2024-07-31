using Microsoft.AspNetCore.Mvc;
using PontoAPonto.Domain.Dtos.Requests.Drivers;
using PontoAPonto.Domain.Errors.Business;
using PontoAPonto.Domain.Interfaces.Repositories;
using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Domain.Interfaces.UseCase;
using PontoAPonto.Domain.Models;
using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Service.UseCases
{
    public class DriverUseCase : IDriverUseCase
    {
        private readonly IDriverService _driverService;
        private readonly IS3Repository _s3Repository;

        public DriverUseCase(IDriverService driverService, IS3Repository s3Repository)
        {
            _driverService = driverService;
            _s3Repository = s3Repository;
        }

        public async Task<CustomActionResult> CaptureProfilePicture([FromBody] CaptureProfilePictureRequest request, string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return DriverError.Unauthorized();
            }

            var driver = await _driverService.GetDriverByEmailAsync(email);

            if (!driver.Success)
            {
                return driver.Error;
            }

            var path = $"{S3.ProfilePicturesDir}{S3.DriverDir}";
            var s3Result = await _s3Repository.UploadFileAsync(S3.BucketName, path, driver.Value.Id.ToString());

            return s3Result;
        }
    }
}
