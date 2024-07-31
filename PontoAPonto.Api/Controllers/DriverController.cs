using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoAPonto.Domain.Dtos.Requests.Drivers;
using PontoAPonto.Domain.Dtos.Responses.User;
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
        [ProducesResponseType(typeof(UserProfileResponse), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(UserProfileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<CustomActionResult> CaptureDocumentPicture([FromBody] CapturePictureRequest request)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            return await _driverUseCase.CaptureDocumentPictureAsync(request, email);
        }
    }
}
