﻿namespace PontoAPonto.Domain.Dtos.Requests
{
    public class ResetPasswordRequest
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
