using Microsoft.AspNetCore.Mvc;
using PontoAPonto.Domain.Dtos.Requests.SignUp;
using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Api.Controllers
{
    [ApiController]
    [Route("api/v1/signup")]
    public class SignUpController : ControllerBase
    {
        private readonly ISignUpService _signUpService;

        public SignUpController(ISignUpService signUpService)
        {
            _signUpService = signUpService;
        }

        [HttpPost("user")]
        [ProducesResponseType(typeof(BaseResponse<SignUpResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<CustomActionResult> CreateUserSignUp(SignUpRequest request)
        {
            return await _signUpService.CreateUserSignUpAsync(request);
        }

        [HttpPost("driver")]
        [ProducesResponseType(typeof(BaseResponse<SignUpResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<CustomActionResult> CreateDriverSignUp(SignUpRequest request)
        {
            return await _signUpService.CreateDriverSignUpAsync(request);
        }
    }
}
