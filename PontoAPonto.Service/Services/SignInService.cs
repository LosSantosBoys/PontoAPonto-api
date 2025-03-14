﻿using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Domain.Models;
using PontoAPonto.Domain.Errors.Business;
using PontoAPonto.Domain.Enums;

namespace PontoAPonto.Service.Services
{
    public class SignInService : ISignInService
    {
        private readonly IUserService _userService;
        private readonly IDriverService _driverService;
        private readonly IAuthService _authService;

        public SignInService(IUserService userService, IDriverService driverService, IAuthService authService)
        {
            _userService = userService;
            _driverService = driverService;
            _authService = authService;
        }

        public async Task<CustomActionResult<SignInResponse>> SignInAsync(SignInRequest request)
        {
            var userResult = await _userService.GetUserByEmailAsync(request.Email);

            if (!userResult.Success)
            {
                return userResult.Error;
            }

            var passwordMatch = userResult.Value.VerifyPasswordHash(request.Password);

            if (!passwordMatch)
            {
                return SignInError.SignInDoesNotMatch();
            }

            var token = _authService.GenerateJwtToken(userResult.Value.Email, "USER");

            var responseData = new SignInResponse { TokenType = "Bearer", Token = token, IsFirstAccess = userResult.Value.IsFirstAccess };

            if (userResult.Value.IsFirstAccess)
            {
                userResult.Value.IsFirstAccess = false;
                await _userService.UpdateUserAsync(userResult.Value);
            }

            return responseData;
        }

        public async Task<CustomActionResult<SignInResponse>> SignInDriverAsync(SignInRequest request)
        {
            var driverResult = await _driverService.GetDriverByEmailAsync(request.Email);

            if (!driverResult.Success)
            {
                return driverResult.Error;
            }

            var passwordMatch = driverResult.Value.VerifyPasswordHash(request.Password);

            if (!passwordMatch)
            {
                return SignInError.SignInDoesNotMatch();
            }

            if (driverResult.Value.Status == DriverStatus.WAITING_OTP_VERIFICATION ||driverResult.Value.Status == DriverStatus.OTP_BLOCKED)
            {
                return SignInError.OtpNotCompleted;
            }

            if (driverResult.Value.Status == DriverStatus.REPROVED)
            {
                return SignInError.ReprovedUser;
            }

            //TODO - Think in some way to let driver login if needs to capture face/document picture but block if waiting for manual validation,
            // maybe using a 200 response w/ actions

            var token = _authService.GenerateJwtToken(driverResult.Value.Email, "DRIVER");

            var responseData = new SignInResponse { TokenType = "Bearer", Token = token, IsFirstAccess = driverResult.Value.IsFirstAccess };

            if (driverResult.Value.IsFirstAccess)
            {
                driverResult.Value.IsFirstAccess = false;
                await _driverService.UpdateDriverAsync(driverResult.Value);
            }

            return responseData;
        }
    }
}
