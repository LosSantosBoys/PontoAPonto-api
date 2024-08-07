using PontoAPonto.Domain.Errors.Business;

namespace PontoAPonto.Domain.Models
{
    public class Otp
    {
        public Otp()
        {
            Password = new Random().Next(1000, 9999);
            Expiracy = DateTime.UtcNow.AddMinutes(5);
            Attempts = 0;
            IsVerified = false;
        }
        private readonly int maxAttempts = 5;
        public int Password { get; private set; }
        public DateTime Expiracy { get; private set; }
        public int Attempts { get; private set; }
        public bool IsVerified { get; private set; }

        public CustomActionResult ValidateOtp(int otpCode)
        {
            if (IsVerified)
            {
                return OtpError.UserAlreadyVerified;
            }

            if (Attempts >= maxAttempts)
            {
                return OtpError.ExceededMaximumAttempts;
            }

            if (DateTime.UtcNow > Expiracy)
            {
                Attempts++;
                return OtpError.ExpiredOtp;
            }

            if (Password != otpCode)
            {
                Attempts++;
                return OtpError.InvalidOtp;
            }

            IsVerified = true;
            Attempts = 0;
            return new CustomActionResult();
        }

        public CustomActionResult GenerateNewOtp()
        {
            if (Attempts > maxAttempts)
            {
                return OtpError.ExceededMaximumAttempts;
            }

            IsVerified = false;
            Password = new Random().Next(1000, 9999);
            Expiracy = DateTime.UtcNow.AddMinutes(10);
            return new CustomActionResult();
        }
    }
}