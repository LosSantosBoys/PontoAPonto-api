using PontoAPonto.Domain.Helpers;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Domain.Errors.Database
{
    public static class DriverRepositoryError
    {
        private const string Entity = "Driver";
        public static CustomError DriverNotFound() => ErrorHelper.CreateEntityNotFoundError(Entity);
    }
}
