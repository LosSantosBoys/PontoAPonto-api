using System.Net;
using System.Text;
using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Errors;
using PontoAPonto.Domain.Helpers;
using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Domain.Models;
using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Service.Services
{
    public class SignUpService : ISignUpService
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public SignUpService(IUserService userService, IAuthService authService, IEmailService emailService)
        {
            _userService = userService;
            _authService = authService;
            _emailService = emailService;
        }

        public async Task<CustomActionResult> CreateUserSignUpAsync(SignUpRequest request)
        {
            var parsedBirthday = DateHelper.ConvertStringToDateTimeddMMyyyy(request.Birthday);

            if (!parsedBirthday.isSuccess)
            {
                return SignUpError.InvalidDateFormat();
            }

            var validateDuplicate = await _userService.ValidateDuplicateUserAsync(request.Email, request.Phone, request.Cpf);

            if (!validateDuplicate.Success)
            {
                return validateDuplicate.Error;
            }

            _authService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = request.ToEntity(passwordHash, passwordSalt, parsedBirthday.dateTime);

            var resultAddUser = await _userService.AddUserAsync(user);

            if (!resultAddUser.Success)
            {
                return resultAddUser.Error;
            }

            var body = new StringBuilder().AppendFormat(Email.Html.BodySignUp, user.Name, user.Otp.Password).ToString();
            var resultSendEmail = await _emailService.SendEmailAsync(user.Email, Email.SubjectOtp, body);

            if (!resultSendEmail.Success)
            {
                await _userService.DeleteUserByEmailAsync(request.Email);
                return resultSendEmail.Error;
            }

            return new CustomActionResult(HttpStatusCode.Created);
        }
    }
}
