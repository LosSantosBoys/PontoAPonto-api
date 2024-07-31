using System.Net;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace PontoAPonto.Domain.Models
{
    public class CustomActionResult : IActionResult
    {
        public bool Success { get; }
        public CustomError Error { get; }
        public HttpStatusCode StatusCode { get; }

        protected CustomActionResult(CustomError error)
        {
            Error = error ?? throw new ArgumentNullException(nameof(error));
            Success = false;
            StatusCode = error.StatusCode;
        }

        public CustomActionResult(HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Success = true;
            StatusCode = statusCode;
            Error = new CustomError(HttpStatusCode.OK, "NoError", "");
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            ObjectResult result;

            if (Success)
            {
                result = new ObjectResult(null)
                {
                    StatusCode = (int?)StatusCode ?? 204
                };
            }
            else
            {
                result = new ObjectResult(Error)
                {
                    StatusCode = (int?)Error?.StatusCode ?? 400
                };
            }

            await result.ExecuteResultAsync(context);
        }

        public static CustomActionResult NoContent()
        {
            return new CustomActionResult(HttpStatusCode.NoContent);
        }

        public static implicit operator CustomActionResult(CustomError error)
        {
            return new CustomActionResult(error);
        }
    }

    public class CustomActionResult<T> : CustomActionResult
    {
        public T Value { get; }

        private CustomActionResult(T value, HttpStatusCode statusCode = HttpStatusCode.OK)
            : base(statusCode)
        {
            Value = value;
        }

        private CustomActionResult(CustomError error)
            : base(error)
        {
            Value = default!;
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

        public async new Task ExecuteResultAsync(ActionContext context)
        {
            ObjectResult result;

            if (Success)
            {
                result = new ObjectResult(Value)
                {
                    StatusCode = (int?)StatusCode ?? 200
                };
            }
            else
            {
                result = new ObjectResult(Error)
                {
                    StatusCode = (int?)Error?.StatusCode ?? 400
                };
            }

            await result.ExecuteResultAsync(context);
        }
    }

    public class CustomError
    {
        public CustomError(HttpStatusCode statusCode, string code, string message, object? value = default)
        {
            StatusCode = statusCode;
            Code = code;
            Message = message;
            Value = value;
        }

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; }
        public string Code { get; }
        public string Message { get; }
        public object? Value { get; }
    }
}
