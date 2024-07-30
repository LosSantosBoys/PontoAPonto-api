using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Domain.Models.Entities
{
    public class Driver : User
    {
        public string? UrlProfilePicture { get; private set; }
        public string? UrlCnhPicture { get; private set; }
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
                UrlProfilePicture = null,
                UrlCnhPicture = null,
                Approved = false,
                ApprovedAt = null
            };
        }

        public void CaptureProfilePicture(string pfpUrl)
        {
            UrlProfilePicture = pfpUrl;
        }

        public void CaptureCnhPicture(string cnhUrl)
        {
            UrlCnhPicture = cnhUrl;
        }

        public void SetCarInfo(string model, string year, string plate, string color)
        {
            CarInfo = new CarInfo(model, year, plate, color);
        }

        public bool AproveDriver()
        {
            if (string.IsNullOrEmpty(UrlProfilePicture) || string.IsNullOrEmpty(UrlCnhPicture))
            {
                return false;
            }

            Approved = true;
            ApprovedAt = DateTime.Now;
            return true;
        }
    }
}
