using AutoMapper;
using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Interfaces.Repositories;
using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Domain.Models;
using PontoAPonto.Domain.Models.Entities;
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

        public async Task<CustomActionResult> AddUserAsync(User user)
        {
            return await _userRepository.AddUserAsync(user);
        }

        public async Task<CustomActionResult> ValidateDuplicateUserAsync(string email, string phone, string cpf)
        {
            return await _userRepository.ValidateDuplicateUserAsync(email, phone, cpf);
        }

        public async Task<CustomActionResult> DeleteUserByEmailAsync(string email)
        {
            return await _userRepository.DeleteUserByEmailAsync(email);
        }

        public async Task<CustomActionResult<User>> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<CustomActionResult> UpdateUserAsync(User user)
        {
            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user.Value.PasswordHash == null)
                return false;

            user.Value.PasswordResetToken = _authService.CreateRandomToken();
            user.Value.ResetTokenExpiracy = DateTime.UtcNow.AddMinutes(30);

            var success = await _userRepository.UpdateUserAsync(user);

            if (success.Success)
            {
                var resetUrl = $"api/v1/user/reset-password?token={user.Value.PasswordResetToken}";
                var body = new StringBuilder().AppendFormat(Email.BodyForgotPassword, resetUrl).ToString();
                await SendEmailAsync(email, Email.SubjectOtp, body);
            }

            return success.Success;
        }

        public async Task<bool> ResetPasswordAsync(string token, ResetPasswordRequest request)
        {
            if (request.Password != request.ConfirmPassword)
                return false;

            var user = await _userRepository.GetUserByTokenAsync(token);

            if (DateTime.UtcNow > user.ResetTokenExpiracy)
                return false;

            _authService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.ChangePassword(passwordHash, passwordSalt);

            await _userRepository.UpdateUserAsync(user);

            return true;
        }

        private async Task<CustomActionResult> SendEmailAsync(string email, string subject, string body)
        {
            return await _emailService.SendEmailAsync(email, subject, body);
        }
    }
}
