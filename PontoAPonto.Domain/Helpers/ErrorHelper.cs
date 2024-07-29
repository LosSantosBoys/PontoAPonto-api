using PontoAPonto.Domain.Models;
using static PontoAPonto.Domain.Constant.Constants;
using System.Net;

namespace PontoAPonto.Domain.Helpers
{
    public static class ErrorHelper
    {
        public static string FormatMessage(string message, string entityName)
        {
            return string.Format(message, entityName);
        }

        public static CustomError CreateEntityNotFoundError(string entityName)
        {
            var errorCode = FormatMessage(ErrorCodes.Generic.Database.EntityNotFound, entityName);
            var errorMessage = FormatMessage(ErrorMessages.Generic.Database.EntityNotFound, entityName);
            return new CustomError(HttpStatusCode.BadRequest, errorCode, errorMessage);
        }
    }
}
