using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Interfaces.Repositories;
using PontoAPonto.Domain.Interfaces.Services;
using System.Net;
using System.Text;
using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public UserService(IUserRepository userRepository, IEmailService emailService, IMapper mapper, IAuthService authService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<BaseResponse<OtpUserResponse>> CreateUserOtpAsync(OtpUserRequest request)
        {
            //TODO: Proper error messages + check for duplicity in email and phone
            var user = request.ToEntity();
            var response = new BaseResponse<OtpUserResponse>();
            var success = await _userRepository.AddUserAsync(user);

            if (!success)
                return response.CreateError(HttpStatusCode.BadRequest, ResponseMessages.ErrorCreatingUserOtp);

            await SendEmailAsync(user.Email, Email.SubjectOtp, user.Otp.Password.ToString());
            return response.CreateSuccess(HttpStatusCode.Created, ResponseMessages.UserOtpCreated, new OtpUserResponse { OtpCode = user.Otp.Password });
        }

        public async Task<bool> ValidateOtpAsync(ValidateOtpRequest request)
        {
            //TODO: Error messages for expiracy, invalid code, db error, etc
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            var isValid = user.ValidateOtp(request.Otp);

            await _userRepository.UpdateUserAsync(user);

            return isValid;
        }

        public async Task<bool> GenerateNewOtpAsync(string email)
        {
            //TODO: Error messages
            var user = await _userRepository.GetUserByEmailAsync(email);

            var success = user.GenerateNewOtp();

            await _userRepository.UpdateUserAsync(user);

            if (success)
            {
                var body = new StringBuilder().AppendFormat(Email.BodyOtp, user.Otp.Password).ToString();
                await SendEmailAsync(email, Email.SubjectOtp, body);
            }

            return success;
        }

        public async Task<bool> FinishSignUpAsync(FinishSignupRequest request)
        {
            //TODO: Error messages + jwt auth?
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            _authService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var success = user.UpdateVerifiedUser(passwordHash, passwordSalt, request.Cpf, request.Birthday);

            if(success)
                await _userRepository.UpdateUserAsync(user);

            return success;
        }

        public async Task<BaseResponse<SignInResponse>> SignInAsync(SignInRequest request)
        {
            //TODO: Error messages
            var response = new BaseResponse<SignInResponse>();
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            var passwordMatch = user.VerifyPasswordHash(request.Password);

            if (!passwordMatch)
                return response.CreateError(HttpStatusCode.BadRequest, ResponseMessages.SignInError);

            var token = _authService.GenerateJwtToken();

            var responseData = new SignInResponse { TokenType = "Bearer", Token = token, IsFirstAccess = user.IsFirstAccess };

            if (user.IsFirstAccess)
            {
                user.IsFirstAccess = false;
                await _userRepository.UpdateUserAsync(user);
            }

            return response.CreateSuccess(HttpStatusCode.Created, ResponseMessages.SignInSuccess, responseData);
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user.PasswordHash.IsNullOrEmpty())
                return false;

            user.PasswordResetToken = _authService.CreateRandomToken();
            user.ResetTokenExpiracy = DateTime.Now.AddMinutes(30);

            var success = await _userRepository.UpdateUserAsync(user);

            if (success)
            {
                var resetUrl = $"api/v1/user/reset-password?token={user.PasswordResetToken}";
                var body = new StringBuilder().AppendFormat(Email.BodyForgotPassword, resetUrl).ToString();
                await SendEmailAsync(email, Email.SubjectOtp, body);
            }

            return success;
        }

        public async Task<bool> ResetPasswordAsync(string token, ResetPasswordRequest request)
        {
            if (request.Password != request.ConfirmPassword)
                return false;

            var user = await _userRepository.GetUserByTokenAsync(token);

            if (DateTime.Now > user.ResetTokenExpiracy)
                return false;

            _authService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.ChangePassword(passwordHash, passwordSalt);

            await _userRepository.UpdateUserAsync(user);

            return true;
        }

        private async Task SendEmailAsync(string email, string subject, string body)
        {
            //TODO: Error messages
            await _emailService.SendEmailAsync(email, subject, body);
        }
    }
}
