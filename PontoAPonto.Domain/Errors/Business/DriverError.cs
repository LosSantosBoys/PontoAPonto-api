using PontoAPonto.Domain.Helpers;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Domain.Errors.Business
{
    public class DriverError
    {
        public const string Entity = "Driver";
        public static CustomError Unauthorized() => ErrorHelper.CreateUnauthorizedError(Entity);
    }
}
