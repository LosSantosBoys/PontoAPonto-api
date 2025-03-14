﻿using PontoAPonto.Domain.Helpers;
using PontoAPonto.Domain.Models;
using System.Net;
using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Domain.Errors.Business
{
    public static class UserError
    {
        public const string Entity = "User";
        public static CustomError Unauthorized() => ErrorHelper.CreateEntityNotFoundError(Entity);
    }
}
