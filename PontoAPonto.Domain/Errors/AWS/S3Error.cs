using PontoAPonto.Domain.Models;
using System.Net;
using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Domain.Errors.AWS
{
    public static class S3Error
    {
        public static CustomError UploadFail() => new CustomError(
            HttpStatusCode.InternalServerError, ErrorCodes.Aws.UploadFail, ErrorMessages.Aws.UploadFail);

        public static CustomError GetFileFail() => new CustomError(
            HttpStatusCode.InternalServerError, ErrorCodes.Aws.GetFileFail, ErrorMessages.Aws.GetFileFail);

        public static CustomError FileNotFound() => new CustomError(
            HttpStatusCode.NotFound, ErrorCodes.Aws.GetFileFail, ErrorMessages.Aws.GetFileFail);
    }
}
