using AutoMapper;
using PontoAPonto.Domain.Dtos.Requests.Drivers;
using PontoAPonto.Domain.Dtos.Responses.Driver;
using PontoAPonto.Domain.Helpers;
using PontoAPonto.Domain.Interfaces.Repositories;
using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Domain.Models;
using PontoAPonto.Domain.Models.Configs;
using PontoAPonto.Domain.Models.Entities;
using System.Text;
using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Service.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IS3Repository _s3Repository;
        private readonly IMapper _mapper;
        private readonly S3Config _s3Config;

        public DriverService(IDriverRepository driverRepository, IS3Repository s3Repository, IMapper mapper, S3Config s3Config)
        {
            _driverRepository = driverRepository;
            _s3Repository = s3Repository;
            _mapper = mapper;
            _s3Config = s3Config;
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

        public async Task<CustomActionResult> CaptureFaceValidationPictureAsync(string email, string imageBase64)
        {
            var driver = await GetDriverByEmailAsync(email);

            if (!driver.Success)
            {
                return driver.Error;
            }

            var path = $"{_s3Config.FaceValidationPicturesDir}{_s3Config.DriversDir}/{driver.Value.Id.ToString()}.png";
            var s3Result = await _s3Repository.UploadFileAsync(_s3Config.BucketName, imageBase64, path);

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

            var path = $"{_s3Config.DocumentPicturesDir}{_s3Config.DriversDir}/{driver.Value.Id.ToString()}.png";
            var s3Result = await _s3Repository.UploadFileAsync(_s3Config.BucketName, imageBase64, path);

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

        public async Task<CustomActionResult> CaptureCarLicenseAsync(string email, string pdfBase64)
        {
            var driverResult = await GetDriverByEmailAsync(email);

            if (!driverResult.Success)
            {
                return driverResult.Error;
            }

            var path = $"{_s3Config.CarLicenseDir}{_s3Config.DriversDir}/{driverResult.Value.Id.ToString()}.pdf";

            var s3Result = await _s3Repository.UploadFileAsync(_s3Config.BucketName, pdfBase64, path);

            if (!s3Result.Success)
            {
                return s3Result.Error;
            }

            var pdfHelper = new PdfHelper();
            var carInfo = pdfHelper.ExtractCrlvData(path);

            driverResult.Value.SetCarInfo(carInfo);

            var updateResult = await UpdateDriverAsync(driverResult.Value);

            if (!updateResult.Success)
            {
                return updateResult.Error;
            }

            return CustomActionResult.NoContent();
        }

        public async Task<CustomActionResult<DriverProfileResponse>> GetDriverProfileAsync(string email)
        {
            var driverResult = await GetDriverByEmailAsync(email);

            if (!driverResult.Success)
            {
                return driverResult.Error;
            }

            var driver = _mapper.Map<DriverProfileResponse>(driverResult.Value);

            return driver;
        }

        public async Task<CustomActionResult> ChangeProfileAsync(ChangeProfileRequest request, string email)
        {
            var driverResult = await GetDriverByEmailAsync(email);

            if (!driverResult.Success)
            {
                return driverResult.Error;
            }

            bool hasDatabaseChanges = false;

            if (!string.IsNullOrWhiteSpace(request.ProfilePictureBase64))
            {
                hasDatabaseChanges = true;
                var resultProfilePicture = await ChangeProfilePictureAsync(driverResult.Value.Id.ToString(), request.ProfilePictureBase64);

                if (!resultProfilePicture.Success)
                {
                    return resultProfilePicture;
                }

                driverResult.Value.UpdateProfilePicture(resultProfilePicture.Value);
            }

            if (request.Location != null)
            {
                hasDatabaseChanges = true;
                driverResult.Value.UpdateLocation(request.Location);
            }

            if (hasDatabaseChanges)
            {
                var updateResult = await UpdateDriverAsync(driverResult.Value);

                if (!updateResult.Success)
                {
                    return updateResult.Error;
                }

                return CustomActionResult.NoContent();
            }

            return CustomActionResult.NoContent();
        }

        private async Task<CustomActionResult<string>> ChangeProfilePictureAsync(string driverId, string base64)
        {
            var path = $"{_s3Config.ProfilePicturesDir}{_s3Config.DriversDir}/{driverId.ToString()}.png";
            var s3Result = await _s3Repository.UploadPublicFileAsync(_s3Config.BucketName, base64, path);

            if (!s3Result.Success)
            {
                return s3Result.Error;
            }

            var sb = new StringBuilder();
            var url = sb.AppendFormat(S3.PublicProfilePictureUrl, _s3Config.BucketName, path).ToString();

            return url;
        }
    }
}
