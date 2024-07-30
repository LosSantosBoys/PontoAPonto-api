using PontoAPonto.Domain.Models;
using System.Net;
using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Domain.Errors.Business
{
    public static class UserError
    {
        public static CustomError Unauthorized() => new CustomError(
            HttpStatusCode.Unauthorized, ErrorMessages.Business.User.Unauthorized, ErrorMessages.Business.User.Unauthorized);
    }
}
