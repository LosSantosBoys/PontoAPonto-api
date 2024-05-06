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

        [HttpPost("signup")]
        [ProducesResponseType(typeof(BaseResponse<SignUpResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUserSignUp(SignUpRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _userService.CreateUserSignUpAsync(request);
            return StatusCode((int)response.StatusCode, response);        
        }

        [HttpPatch("signup/otp/validate")]
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

        [HttpPatch("signup/otp/new")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GenerateNewOtp(GenerateNewOtpRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _userService.GenerateNewOtpAsync(request.Email);

            return Ok(response);
        }

        [HttpPost("signin")]
        [ProducesResponseType(typeof(BaseResponse<SignInResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _userService.SignInAsync(request);

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPatch("forgot-password")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            var response = await _userService.ForgotPasswordAsync(email);


            return Ok(response);
        }

        [HttpPatch("reset-password")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword(string token, [FromBody] ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _userService.ResetPasswordAsync(token, request);

            return Ok(response);
        }
    }
}