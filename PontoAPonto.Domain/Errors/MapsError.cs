using PontoAPonto.Domain.Models;
using System.Net;
using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Domain.Errors
{
    public static class MapsError
    {
        public static CustomError HttpError(HttpStatusCode statusCode, string message) 
        { 
            return new CustomError(statusCode, ErrorCodes.HttpError, message); 
        }
    }
}
