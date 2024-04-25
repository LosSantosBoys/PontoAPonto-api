﻿using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Dtos.Responses;

namespace PontoAPonto.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<BaseResponse<OtpUserResponse>> CreateUserOtpAsync(OtpUserRequest request);
        Task<bool> ValidateOtpAsync(ValidateOtpRequest request);
        Task<bool> GenerateNewOtpAsync(string email);
    }
}
