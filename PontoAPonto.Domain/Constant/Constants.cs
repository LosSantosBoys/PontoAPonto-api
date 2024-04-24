namespace PontoAPonto.Domain.Constant
{
    public static class Constants
    {
        public struct UserStatus
        {
            public const string WaitingOtpVerification = "Waiting for OTP verification";
            public const string OtpVerified = "OTP verified";
        }

        public struct ResponseMessages
        {
            public const string UserOtpCreated = "User and OTP code created";
            public const string ErrorCreatingUserOtp = "Failed to create user.";
        }

        public struct Email
        {
            public const string SubjectOtp = "Ponto a Ponto - Código de verificação";
            public const string BodyOtp = "Seu código de verificação: {0}";
        }
    }
}
