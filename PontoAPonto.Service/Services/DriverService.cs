using PontoAPonto.Domain.Interfaces.Repositories;
using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Domain.Models;
using PontoAPonto.Domain.Models.Entities;
using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Service.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IS3Repository _s3Repository;

        public DriverService(IDriverRepository driverRepository, IS3Repository s3Repository)
        {
            _driverRepository = driverRepository;
            _s3Repository = s3Repository;
        }

        public async Task<CustomActionResult> AddDriverAsync(Driver driver)
        {
            return await _driverRepository.AddDriverAsync(driver);
        }

        public async Task<CustomActionResult> DeleteDriverByEmailAsync(string email)
        {
            return await _driverRepository.DeleteDriverByEmailAsync(email);
        }

        public async Task<CustomActionResult<Driver>> GetDriverByEmailAsync(string email)
        {
            return await _driverRepository.GetDriverByEmailAsync(email);
        }

        public async Task<CustomActionResult> UpdateDriverAsync(Driver driver)
        {
            driver.UpdatedAt = DateTime.Now;
            return await _driverRepository.UpdateDriverAsync(driver);
        }

        public async Task<CustomActionResult> CaptureProfilePictureAsync(string email, string imageBase64)
        {
            var driver = await GetDriverByEmailAsync(email);

            if (!driver.Success)
            {
                return driver.Error;
            }

            var path = $"{S3.ProfilePicturesDir}{S3.DriverDir}/{driver.Value.Id.ToString()}.png";
            var s3Result = await _s3Repository.UploadFileAsync(S3.BucketName, imageBase64, path);

            if (s3Result.Success)
            {
                return s3Result.Error;
            }

            driver.Value.CaptureFacePicture();

            var updateResult = await UpdateDriverAsync(driver.Value);

            if (!updateResult.Success)
            {
                return updateResult.Error;
            }

            return CustomActionResult.NoContent();
        }

        public async Task<CustomActionResult> CaptureDocumentPictureAsync(string email, string imageBase64)
        {
            var driver = await GetDriverByEmailAsync(email);

            if (!driver.Success)
            {
                return driver.Error;
            }

            var path = $"{S3.DocumentPicturesDir}{S3.DriverDir}/{driver.Value.Id.ToString()}.png";
            var s3Result = await _s3Repository.UploadFileAsync(S3.BucketName, imageBase64, path);

            if (s3Result.Success)
            {
                return s3Result.Error;
            }

            driver.Value.CaptureDocumentPicture();

            var updateResult = await UpdateDriverAsync(driver.Value);

            if (!updateResult.Success)
            {
                return updateResult.Error;
            }

            return CustomActionResult.NoContent();
        }

        public async Task<CustomActionResult> InsertCarInfoAsync(CarInfo request, string email)
        {
            var driverResult = await GetDriverByEmailAsync(email);

            if (!driverResult.Success)
            {
                return driverResult.Error;
            }

            driverResult.Value.SetCarInfo(request);

            var updateResult = await UpdateDriverAsync(driverResult.Value);

            if (!updateResult.Success)
            {
                return updateResult.Error;
            }

            return CustomActionResult.NoContent();
        }
    }
}
