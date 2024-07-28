using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Domain.Models.Entities
{
    public class Driver : User
    {
        public static new Driver CreateUser(string name, string email, string phone, byte[] passwordHash, byte[] passwordSalt, string cpf, DateTime birthday)
        {
            return new Driver
            {
                Name = name,
                Email = email,
                Phone = phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Cpf = cpf,
                Birthday = birthday,
                Otp = new Otp(),
                Status = UserStatus.WaitingOtpVerification,
                IsFirstAccess = true
            };
        }
    }
}
