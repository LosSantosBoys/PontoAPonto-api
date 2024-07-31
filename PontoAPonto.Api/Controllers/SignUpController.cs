using Microsoft.AspNetCore.Mvc;
using PontoAPonto.Domain.Dtos.Requests.SignUp;
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status500InternalServerError)]
        public async Task<CustomActionResult> CreateUserSignUp([FromBody] SignUpRequest request)
        {
            return await _signupUseCase.CreateSignUpAsync(request, UserType.USER);
        }

        [HttpPost("driver")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status500InternalServerError)]
        public async Task<CustomActionResult> CreateDriverSignUp([FromBody] SignUpRequest request)
        {
            return await _signupUseCase.CreateSignUpAsync(request, UserType.DRIVER);
        }

        [HttpPatch("otp/validate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status500InternalServerError)]
        public async Task<CustomActionResult> ValidateOtp([FromBody] ValidateOtpRequest request)
        {
            return await _signupUseCase.ValidateOtpAsync(request);
        }

        [HttpPatch("otp/new")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateNewOtp([FromBody] CreateNewOtpRequest request)
        {
            return await _signupUseCase.CreateNewOtpAsync(request);
        }
    }
}
