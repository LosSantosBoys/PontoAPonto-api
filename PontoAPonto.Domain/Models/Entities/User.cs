using System.ComponentModel.DataAnnotations;
using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Domain.Models.Entities
{
    public class User : EntityBase
    {
        [MaxLength(255)]
        public string Name { get; private set; }

        [MaxLength(255)]
        public string Email { get; private set; }

        [MaxLength(13)]
        public string Phone { get; private set; }
        public Otp Otp { get; private set; }

        [MaxLength(255)]
        public string Status { get; private set; }
        public string? PasswordHash { get; private set; }
        public string? PasswordSalt { get; private set; }

        [MaxLength(11)]
        public string? Cpf { get; private set; }
        public DateTime? Birthday {  get; private set; }

        public User CreateUser(string name, string email, string phone)
        {
            return new User
            {
                Name = name,
                Email = email,
                Phone = phone,
                Otp = new Otp(),
                Status = UserStatus.WaitingOtpVerification
            };
        }

        public void VerifyUser(string passwordHash, string passwordSalt, string cpf, DateTime birthday)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Cpf = cpf;
            Birthday = birthday;
            Status = UserStatus.OtpVerified;
        }
    }
}
