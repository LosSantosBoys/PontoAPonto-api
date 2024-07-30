using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Domain.Models.Entities
{
    public class Driver : User
    {
        public CarInfo? CarInfo { get; private set; }
        public bool Approved { get; private set; }
        public DateTime? ApprovedAt { get; private set; }

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
                IsFirstAccess = true,
                Reputation = 5,
                Approved = false,
                ApprovedAt = null
            };
        }

        public void SetCarInfo(string model, string year, string plate, string color)
        {
            CarInfo = new CarInfo(model, year, plate, color);
        }

        public bool AproveDriver()
        {
            Approved = true;
            ApprovedAt = DateTime.Now;
            return true;
        }
    }
}
