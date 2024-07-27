using PontoAPonto.Domain.Models;
using System.Net;
using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Domain.Errors
{
    public static class EmailError
    {
        public static CustomError SendEmailError() => new CustomError(
            HttpStatusCode.InternalServerError, ErrorCodes.Generic.InternalError, ErrorMessages.Generic.InternalError);
    }
}
