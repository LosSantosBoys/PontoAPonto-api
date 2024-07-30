using AutoMapper;
using PontoAPonto.Domain.Dtos.Responses.User;
using PontoAPonto.Domain.Errors.Business;
using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Domain.Interfaces.UseCase;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Service.UseCases
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserUseCase(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<UserProfileResponse>> GetUserProfileAsync(string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return UserError.Unauthorized();
            }

            var user = await _userService.GetUserByEmailAsync(email);

            if (!user.Success)
            {
                return user.Error;
            }

            var profile = _mapper.Map<UserProfileResponse>(user);
            return profile;
        }
    }
}
