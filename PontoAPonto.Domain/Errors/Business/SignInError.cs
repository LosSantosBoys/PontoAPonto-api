using PontoAPonto.Domain.Models;
using System.Net;
using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Domain.Errors.Business
{
    public static class SignInError
    {
        public static CustomError SignInDoesNotMatch() => new CustomError(
            HttpStatusCode.BadRequest, ErrorCodes.Business.SignIn.InvalidSignIn, ErrorMessages.Business.SignIn.InvalidSignIn);

        public static CustomError OtpNotCompleted = new CustomError(
            HttpStatusCode.BadRequest, ErrorCodes.Business.SignIn.OtpNotCompleted, ErrorCodes.Business.SignIn.OtpNotCompleted);

        public static CustomError ReprovedUser = new CustomError(
            HttpStatusCode.BadRequest, ErrorCodes.Business.SignIn.ReprovedUser, ErrorCodes.Business.SignIn.ReprovedUser);
    }
}
