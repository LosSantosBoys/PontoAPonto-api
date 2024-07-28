using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using static PontoAPonto.Domain.Constant.Constants;

namespace PontoAPonto.Domain.Models.Entities
{
    public class User : EntityBase
    {
        [MaxLength(255)]
        public string Name { get; protected set; }

        [MaxLength(255)]
        public string Email { get; protected set; }

        [MaxLength(13)]
        public string Phone { get; protected set; }
        public Otp Otp { get; protected set; }
        public bool IsFirstAccess { get; set; }

        [MaxLength(255)]
        public string Status { get; protected set; }
        public byte[]? PasswordHash { get; protected set; }
        public byte[]? PasswordSalt { get; protected set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpiracy { get; set; }
        [MaxLength(11)]
        public string? Cpf { get; protected set; }
        public DateTime? Birthday {  get; protected set; }

        public User CreateUser(string name, string email, string phone, byte[] passwordHash, byte[] passwordSalt, string cpf, DateTime birthday)
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
                Status = UserStatus.WaitingOtpVerification,
                IsFirstAccess = true
            };
        }

        public bool ValidateOtp(int otpCode)
        {
            var success = Otp.SendOtp(otpCode);
            UpdatedAt = DateTime.Now;
            Status = UserStatus.SignInAvailable;

            return success;
        }

        public bool GenerateNewOtp()
        {
            var success = Otp.GenerateNewOtp();
            UpdatedAt = DateTime.Now;

            return success;
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

        public void ChangePassword(byte[] passwordHash, byte[] passwordSalt)
        {
            PasswordResetToken = null;
            ResetTokenExpiracy = null;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
    }
}
