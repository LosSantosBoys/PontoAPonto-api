using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Dtos.Responses.User;
using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Domain.Interfaces.UseCase;
using PontoAPonto.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace PontoAPonto.Api.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserUseCase _userUseCase;

        public UserController(IUserService userService, IUserUseCase userUseCase)
        {
            _userService = userService;
            _userUseCase = userUseCase;
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

        [HttpGet("profile/me")]
        [ProducesResponseType(typeof(UserProfileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<CustomActionResult<UserProfileResponse>> GetProfile()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            return await _userUseCase.GetUserProfileAsync(userEmail);
        }

        [HttpGet("profile/me/delete")]
        [ProducesResponseType(typeof(UserProfileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<CustomActionResult> DeleteAccount()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            return await _userUseCase.DeleteAccountAsync(userEmail);
        }
    }
}