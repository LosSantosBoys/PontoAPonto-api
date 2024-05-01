﻿namespace PontoAPonto.Domain.Constant
{
    public static class Constants
    {
        public struct UserStatus
        {
            public const string WaitingOtpVerification = "Waiting for OTP verification";
            public const string OtpVerified = "OTP verified";
            public const string SignInAvailable = "SignIn Available";
        }

        public struct ResponseMessages
        {
            public const string UserOtpCreated = "User and OTP code created";
            public const string ErrorCreatingUserOtp = "Failed to create user.";
            public const string SignInError = "Email and password does not match.";
            public const string SignInSuccess = "SignIn success";
        }

        public struct Email
        {
            public const string SubjectOtp = "Ponto a Ponto - Código de verificação";
            public const string BodyOtp = "Seu código de verificação: {0}";
            public const string SubjectForgotPassword = "Ponto a Ponto - Recuperação de senha";
            public const string BodyForgotPassword = "Acesse {0} para alterar sua senha";
        }
    }
}
