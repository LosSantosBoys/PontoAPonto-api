using Microsoft.AspNetCore.Mvc;
using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Interfaces.UseCase;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Api.Controllers
{
    [ApiController]
    [Route("api/v1/signin")]
    public class SignInController : ControllerBase
    {
        private readonly ISignInUseCase _signInInUseCase;

        public SignInController(ISignInUseCase signInInUseCase)
        {
            _signInInUseCase = signInInUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomActionResult<SignInResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            return await _signInInUseCase.SignInAsync(request);
        }
    }
}
