using Microsoft.AspNetCore.Mvc;
using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Interfaces.Services;
using System.ComponentModel.DataAnnotations;

namespace PontoAPonto.Api.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/signup")]
        [ProducesResponseType(typeof(BaseResponse<OtpUserResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUserOtp(OtpUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _userService.CreateUserOtpAsync(request);
            return StatusCode((int)response.StatusCode, response.Message);        
        }

        [HttpPatch("/validate-otp")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateOtp(ValidateOtpRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _userService.ValidateOtpAsync(request);

            return Ok(response);
        }

        [HttpPatch("/new-otp")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GenerateNewOtp([FromBody] [Required] string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _userService.GenerateNewOtpAsync(email);

            return Ok(response);
        }
    }
}
