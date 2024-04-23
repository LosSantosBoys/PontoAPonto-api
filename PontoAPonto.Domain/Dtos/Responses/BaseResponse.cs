using System.Net;

namespace PontoAPonto.Domain.Dtos.Responses
{
    public class BaseResponse<T> where T : class
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public T? Data { get; set; }

        public BaseResponse<T> CreateSuccess(HttpStatusCode status, string message, T data) 
        {
            return new BaseResponse<T>
            {
                StatusCode = status,
                Message = message,
                Success = true,
                Data = data
            };
        }

        public BaseResponse<T> CreateError(HttpStatusCode status, string message)
        {
            return new BaseResponse<T>
            {
                StatusCode = status,
                Message = message,
                Success = false,
                Data = null
            };
        }
    }
}