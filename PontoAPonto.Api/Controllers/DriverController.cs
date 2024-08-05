using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoAPonto.Domain.Dtos.Requests.Drivers;
using PontoAPonto.Domain.Dtos.Responses.Driver;
using PontoAPonto.Domain.Interfaces.UseCase;
using PontoAPonto.Domain.Models;
using System.Security.Claims;

namespace PontoAPonto.Api.Controllers
{
    [ApiController]
    [Route("api/v1/driver")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverUseCase _driverUseCase;

        public DriverController(IDriverUseCase driverUseCase)
        {
            _driverUseCase = driverUseCase;
        }

        [HttpPatch("capture/validaton-picture")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<CustomActionResult> CaptureValidationPicture([FromBody] CapturePictureRequest request)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            return await _driverUseCase.CaptureValidationPictureAsync(request, email);
        }

        [HttpPatch("capture/document-picture")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<CustomActionResult> CaptureDocumentPicture([FromBody] CapturePictureRequest request)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            return await _driverUseCase.CaptureDocumentPictureAsync(request, email);
        }

        [HttpPatch("capture/car-license")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<CustomActionResult> CaptureCarLicense([FromBody] CaptureCarLicenseAsync request)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            return await _driverUseCase.CaptureCarLicenseAsync(request, email);
        }

        [HttpGet("profile/me")]
        [ProducesResponseType(typeof(DriverProfileResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<CustomActionResult<DriverProfileResponse>> GetProfile()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            return await _driverUseCase.GetDriverProfileAsync(email);
        }

        [HttpPatch("profile/me")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<CustomActionResult> ChangeProfile(ChangeProfileRequest request)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            return await _driverUseCase.ChangeProfileAsync(request, email);
        }

        [HttpDelete("profile/me/delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<CustomActionResult> DeleteAccount()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            return await _driverUseCase.DeleteAccountAsync(userEmail);
        }
    }
}
