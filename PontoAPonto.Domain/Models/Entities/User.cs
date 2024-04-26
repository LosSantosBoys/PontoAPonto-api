using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
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
        public byte[]? PasswordHash { get; private set; }
        public byte[]? PasswordSalt { get; private set; }

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

        public bool ValidateOtp(int otpCode)
        {
            var success = Otp.SendOtp(otpCode);
            UpdatedAt = DateTime.Now;

            return success;
        }

        public bool GenerateNewOtp()
        {
            var success = Otp.GenerateNewOtp();
            UpdatedAt = DateTime.Now;

            return success;
        }

        public bool UpdateVerifiedUser(byte[] passwordHash, byte[] passwordSalt, string cpf, DateTime birthday)
        {
            if (!Otp.IsVerified)
                return false;

            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Cpf = cpf;
            Birthday = birthday;
            Status = UserStatus.OtpVerified;
            UpdatedAt = DateTime.Now;

            return true;
        }

        public bool VerifyPasswordHash(string password)
        {
            if (PasswordHash == null || PasswordSalt == null)
                return false;

            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(PasswordHash);
            }
        }
    }
}
