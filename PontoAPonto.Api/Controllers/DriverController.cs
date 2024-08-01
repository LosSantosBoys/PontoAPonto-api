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

        [HttpPatch("capture/profile-picture")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<CustomActionResult> CaptureProfilePicture([FromBody] CapturePictureRequest request)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            return await _driverUseCase.CaptureProfilePictureAsync(request, email);
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

        [HttpPatch("car-info")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<CustomActionResult> InsertCarInfo([FromBody] CarInfo request)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            return await _driverUseCase.InsertCarInfoAsync(request, email);
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

        [HttpGet("profile/me")]
        [Authorize]
        public async Task<CustomActionResult<DriverProfileResponse>> GetProfile()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            return await _driverUseCase.GetDriverProfileAsync(email);
        }
    }
}
