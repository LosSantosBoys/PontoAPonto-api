using PontoAPonto.Domain.Models;
using System.Net;
using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Domain.Errors.Business
{
    public static class OtpError
    {
        public static CustomError UserAlreadyVerified = new CustomError(
            HttpStatusCode.BadRequest, ErrorCodes.Business.Otp.UserAlreadyVerified, ErrorMessages.Business.Otp.UserAlreadyVerified);

        public static CustomError ExceededMaximumAttempts = new CustomError(
            HttpStatusCode.BadRequest, ErrorCodes.Business.Otp.ExceededMaximumAttempts, ErrorMessages.Business.Otp.ExceededMaximumAttempts);

        public static CustomError ExpiredOtp = new CustomError(
            HttpStatusCode.BadRequest, ErrorCodes.Business.Otp.ExpiredOtp, ErrorMessages.Business.Otp.ExpiredOtp);

        public static CustomError InvalidOtp = new CustomError(
            HttpStatusCode.BadRequest, ErrorCodes.Business.Otp.InvalidOtp, ErrorMessages.Business.Otp.InvalidOtp);
    }
}
