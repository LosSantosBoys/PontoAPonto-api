using PontoAPonto.Domain.Models;
using System.Net;
using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Domain.Errors
{
    public static class SignUpError
    {
        public static CustomError DataConflict(bool email, bool phone, bool cpf) => new CustomError(
            HttpStatusCode.BadRequest, ErrorCodes.SignUp.DataConflict, ErrorMessages.SignUp.DataConflict, 
            new { Email = email, Phone = phone, Cpf = cpf });

        public static CustomError DatabaseError() => new CustomError(
            HttpStatusCode.InternalServerError, ErrorCodes.Generic.InternalError, ErrorMessages.Generic.InternalError);

        public static CustomError UserNotFound() => new CustomError(
            HttpStatusCode.BadRequest, ErrorCodes.SignUp.UserNotFound, ErrorMessages.SignUp.UserNotFound);

        public static CustomError InvalidDateFormat() => new CustomError(
            HttpStatusCode.BadRequest, ErrorCodes.SignUp.InvalidDateFormat, ErrorMessages.SignUp.InvalidDateFormat);

        public static CustomError InvalidOtp() => new CustomError(
            HttpStatusCode.BadRequest, ErrorCodes.SignUp.InvalidOtp, ErrorMessages.SignUp.InvalidOtp);
    }
}
