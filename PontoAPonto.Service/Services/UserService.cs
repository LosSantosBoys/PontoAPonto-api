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

        public UserService(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<BaseResponse<OtpUserResponse>> CreateUserOtpAsync(OtpUserRequest request)
        {
            //TODO: Proper error messages + check for duplicity in email and phone
            var user = request.ToEntity();
            var response = new BaseResponse<OtpUserResponse>();
            var success = await _userRepository.CreateUserOtpAsync(user);

            if (!success)
                response.CreateError(HttpStatusCode.BadRequest, ResponseMessages.ErrorCreatingUserOtp);
                
            var body = new StringBuilder().AppendFormat(Email.BodyOtp, user.Otp.Password).ToString();

            await _emailService.SendEmailAsync(user.Email, Email.SubjectOtp, body);
            return response.CreateSuccess(HttpStatusCode.Created, ResponseMessages.UserOtpCreated, new OtpUserResponse { OtpCode = user.Otp.Password });
        }
    }
}
