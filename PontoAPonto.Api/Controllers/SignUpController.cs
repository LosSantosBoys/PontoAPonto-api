using Microsoft.AspNetCore.Mvc;
using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Dtos.Requests.SignUp;
using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Enums;
using PontoAPonto.Domain.Interfaces.UseCase;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Api.Controllers
{
    [ApiController]
    [Route("api/v1/signup")]
    public class SignUpController : ControllerBase
    {
        private readonly ISignUpUseCase _signupUseCase;

        public SignUpController(ISignUpUseCase signupUseCase)
        {
            _signupUseCase = signupUseCase;
        }

        [HttpPost("user")]
        [ProducesResponseType(typeof(BaseResponse<SignUpResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<CustomActionResult> CreateUserSignUp(SignUpRequest request)
        {
            return await _signupUseCase.CreateSignUpAsync(request, UserType.USER);
        }

        [HttpPost("driver")]
        [ProducesResponseType(typeof(BaseResponse<SignUpResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<CustomActionResult> CreateDriverSignUp(SignUpRequest request)
        {
            return await _signupUseCase.CreateSignUpAsync(request, UserType.DRIVER);
        }

        [HttpPatch("otp/validate")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateOtp(ValidateOtpRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _signupUseCase.ValidateOtpAsync(request);

            return Ok(response);
        }
    }
}
