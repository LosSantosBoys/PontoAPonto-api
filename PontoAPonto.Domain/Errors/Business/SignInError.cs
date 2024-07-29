using PontoAPonto.Domain.Models;
using System.Net;
using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Domain.Errors.Business
{
    public static class SignInError
    {
        public static CustomError SignInDoesNotMatch() => new CustomError(
            HttpStatusCode.BadRequest, ErrorCodes.Business.SignIn.InvalidSignIn, ErrorMessages.Business.SignIn.InvalidSignIn);
    }
}
