using System.Net;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace PontoAPonto.Domain.Models
{
    public class CustomActionResult<T> : IActionResult
    {
        public bool Success {  get; }
        public CustomError Error { get; }
        public T Value { get; }
        public HttpStatusCode StatusCode { get; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            ObjectResult result = new ObjectResult(Success ? (Value) : Error)
            {
                StatusCode = Success ? (int)StatusCode : ((int?)Error?.StatusCode ?? 400)
            };

            await result.ExecuteResultAsync(context);
        }

        private CustomActionResult(CustomError error)
        {
            Value = default!;
            Error = error ?? throw new ArgumentNullException(nameof(error));
            Success = false;
        }

        private CustomActionResult(T value, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Success = true;
            Value = value;
            StatusCode = statusCode;
            Error = new CustomError(HttpStatusCode.OK, "NoError", "");
        }

        public static implicit operator CustomActionResult<T>(CustomError error) 
        { 
            return new CustomActionResult<T>(error); 
        }

        public static implicit operator CustomActionResult<T>(T value)
        {
            return new CustomActionResult<T>(value);
        }

        public static implicit operator T(CustomActionResult<T> result)
        {
            return result.Value;
        }
    }

    public class CustomError
    {
        public CustomError(HttpStatusCode statusCode, string code, string message)
        {
            StatusCode = statusCode;
            Code = code;
            Message = message;
        }

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; }
        public string Code { get; }
        public string Message { get; }
    }
}
