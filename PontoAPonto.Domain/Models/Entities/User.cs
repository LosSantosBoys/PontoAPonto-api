using PontoAPonto.Domain.Enums;

namespace PontoAPonto.Domain.Models.Entities
{
    public class User : UserBase
    {
        public UserStatus Status { get; protected set; }

        public static User CreateUser(string name, string email, string phone, byte[] passwordHash, byte[] passwordSalt, string cpf, DateTime birthday)
        {
            return new User
            {
                Name = name,
                Email = email,
                Phone = phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Cpf = cpf,
                Birthday = birthday,
                Otp = new Otp(),
                Status = UserStatus.WAITING_OTP_VERIFICATION,
                IsFirstAccess = true,
                Reputation = 5
            };
        }

        public override CustomActionResult ValidateOtp(int otpCode)
        {
            var result = base.ValidateOtp(otpCode);

            if (result.Success)
            {
                Status = UserStatus.SIGNIN_AVAILABLE;
            }

            return result;
        }
    }
}
