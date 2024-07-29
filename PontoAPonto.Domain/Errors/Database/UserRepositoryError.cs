using PontoAPonto.Domain.Helpers;
using PontoAPonto.Domain.Models;

namespace PontoAPonto.Domain.Errors.Database
{
    public static class UserRepositoryError
    {
        private const string Entity = "User";
        public static CustomError UserNotFound() => ErrorHelper.CreateEntityNotFoundError(Entity);
    }
}
