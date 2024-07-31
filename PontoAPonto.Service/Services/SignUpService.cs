using System.Net;
using System.Text;
using PontoAPonto.Domain.Dtos.Requests.SignUp;
using PontoAPonto.Domain.Errors.Business;
using PontoAPonto.Domain.Helpers;
using PontoAPonto.Domain.Interfaces.Services;
using PontoAPonto.Domain.Models;
using PontoAPonto.Domain.Models.Entities;
using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Service.Services
{
    public class SignUpService : ISignUpService
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        private readonly IDriverService _driverService;

        public SignUpService(IUserService userService, IAuthService authService, IEmailService emailService, IDriverService driverService)
        {
            _userService = userService;
            _authService = authService;
            _emailService = emailService;
            _driverService = driverService;
        }

        public async Task<CustomActionResult> CreateUserSignUpAsync(SignUpRequest request)
        {
            var validateRequest = await ValidateRequestAsync(request);

            if (!validateRequest.result.Success)
            {
                return validateRequest.result.Error;
            }

            _authService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = request.ToEntity(passwordHash, passwordSalt, validateRequest.parsedDate);

            var resultAddUser = await _userService.AddUserAsync(user);

            if (!resultAddUser.Success)
            {
                return resultAddUser.Error;
            }

            var resultSendEmail = await SendSignUpEmailAsync(user.Name, user.Email, user.Otp.Password);

            if (!resultSendEmail.Success)
            {
                await _userService.DeleteUserByEmailAsync(request.Email);
                return resultSendEmail.Error;
            }

            return new CustomActionResult(HttpStatusCode.Created);
        }

        public async Task<CustomActionResult> CreateDriverSignUpAsync(SignUpRequest request)
        {
            var validateRequest = await ValidateRequestAsync(request);

            if (!validateRequest.result.Success)
            {
                return validateRequest.result.Error;
            }

            _authService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var driver = request.ToDriverEntity(passwordHash, passwordSalt, validateRequest.parsedDate);

            var resultAddDriver = await _driverService.AddDriverAsync(driver);

            if (!resultAddDriver.Success)
            {
                return resultAddDriver.Error;
            }

            var resultSendEmail = await SendSignUpEmailAsync(driver.Name, driver.Email, driver.Otp.Password);

            if (!resultSendEmail.Success)
            {
                await _driverService.DeleteDriverByEmailAsync(request.Email);
                return resultSendEmail.Error;
            }

            return new CustomActionResult(HttpStatusCode.Created);
        }

        public async Task<CustomActionResult> ValidateDriverOtpAsync(ValidateOtpRequest request)
        {
            var driverResult = await _driverService.GetDriverByEmailAsync(request.Email);

            if (!driverResult.Success)
            {
                return driverResult.Error;
            }

            var isValid = driverResult.Value.ValidateOtp(request.Otp);

            if (!isValid)
            {
                return SignUpError.InvalidOtp();
            }

            var updateResult = await _driverService.UpdateDriverAsync(driverResult);

            if (!updateResult.Success)
            {
                return updateResult.Error;
            }

            return new CustomActionResult(HttpStatusCode.OK);
        }

        public async Task<CustomActionResult> ValidateUserOtpAsync(ValidateOtpRequest request)
        {
            var userResult = await _userService.GetUserByEmailAsync(request.Email);

            if (!userResult.Success)
            {
                return userResult.Error;
            }

            var isValid = userResult.Value.ValidateOtp(request.Otp);

            if (!isValid)
            {
                return SignUpError.InvalidOtp();
            }

            var updateResult = await _userService.UpdateUserAsync(userResult);

            if (!updateResult.Success)
            {
                return updateResult.Error;
            }

            return new CustomActionResult(HttpStatusCode.OK);
        }

        private async Task<(CustomActionResult result, DateTime parsedDate)> ValidateRequestAsync(SignUpRequest request)
        {
            var parsedBirthday = DateHelper.ConvertStringToDateTimeddMMyyyy(request.Birthday);

            if (!parsedBirthday.isSuccess)
            {
                return (SignUpError.InvalidDateFormat(), default);
            }

            var validateDuplicate = await _userService.ValidateDuplicateUserAsync(request.Email, request.Phone, request.Cpf);

            if (!validateDuplicate.Success)
            {
                return (validateDuplicate.Error, default);
            }

            return (CustomActionResult.NoContent(), parsedBirthday.dateTime);
        }

        private async Task<CustomActionResult> SendSignUpEmailAsync(string name, string email, int otpPassword)
        {
            var body = new StringBuilder().AppendFormat(Email.Html.BodySignUp, name, otpPassword).ToString();
            return await _emailService.SendEmailAsync(email, Email.SubjectOtp, body);
        }

        public async Task<CustomActionResult> CreateNewUserOtpAsync(string email)
        {
            var userResult = await _userService.GetUserByEmailAsync(email);

            if (!userResult.Success)
            {
                return userResult.Error;
            }

            var generateOtp = userResult.Value.GenerateNewOtp();

            var updateResult = await _userService.UpdateUserAsync(userResult.Value);

            if (!updateResult.Success)
            {
                return updateResult.Error;
            }

            var emailResult = await SendNewOtpEmailAsync(email, userResult.Value.Otp.Password);

            if (!emailResult.Success)
            {
                return emailResult.Error;
            }

            return new CustomActionResult(HttpStatusCode.OK);
        }

        public async Task<CustomActionResult> CreateNewDriverOtpAsync(string email)
        {
            var driverResult = await _driverService.GetDriverByEmailAsync(email);

            if (!driverResult.Success)
            {
                return driverResult.Error;
            }

            var generateOtp = driverResult.Value.GenerateNewOtp();

            var updateResult = await _driverService.UpdateDriverAsync(driverResult.Value);

            if (!updateResult.Success)
            {
                return updateResult.Error;
            }

            var emailResult = await SendNewOtpEmailAsync(email, driverResult.Value.Otp.Password);

            if (!emailResult.Success)
            {
                return emailResult.Error;
            }

            return new CustomActionResult(HttpStatusCode.OK);
        }

        private async Task<CustomActionResult> SendNewOtpEmailAsync(string email, int otpCode)
        {
            var body = new StringBuilder().AppendFormat(Email.BodyOtp, otpCode).ToString();
            return await _emailService.SendEmailAsync(email, Email.SubjectOtp, body);
        }
    }
}
