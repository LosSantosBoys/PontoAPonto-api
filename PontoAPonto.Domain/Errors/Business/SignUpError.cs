using PontoAPonto.Domain.Models;
using System.Net;
using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Domain.Errors.Business
{
    public static class SignUpError
    {
        public static CustomError DataConflict(bool email, bool phone, bool cpf) => new CustomError(
            HttpStatusCode.BadRequest, ErrorCodes.Business.SignUp.DataConflict, ErrorMessages.Business.SignUp.DataConflict,
            new { Email = email, Phone = phone, Cpf = cpf });

        public static CustomError DatabaseError() => new CustomError(
            HttpStatusCode.InternalServerError, ErrorCodes.Generic.InternalError, ErrorMessages.Generic.InternalError);

        public static CustomError InvalidDateFormat() => new CustomError(
            HttpStatusCode.BadRequest, ErrorCodes.Business.SignUp.InvalidDateFormat, ErrorMessages.Business.SignUp.InvalidDateFormat);

        public static CustomError InvalidOtp() => new CustomError(
            HttpStatusCode.BadRequest, ErrorCodes.Business.SignUp.InvalidOtp, ErrorMessages.Business.SignUp.InvalidOtp);
    }
}
