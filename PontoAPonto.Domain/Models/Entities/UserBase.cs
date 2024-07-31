using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace PontoAPonto.Domain.Models.Entities
{
    public abstract class UserBase : EntityBase
    {
        [MaxLength(255)]
        public string Name { get; protected set; }

        [MaxLength(255)]
        public string Email { get; protected set; }

        [MaxLength(13)]
        public string Phone { get; protected set; }
        public Otp Otp { get; protected set; }
        public bool IsFirstAccess { get; set; }
        public byte[]? PasswordHash { get; protected set; }
        public byte[]? PasswordSalt { get; protected set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpiracy { get; set; }
        [MaxLength(11)]
        public string? Cpf { get; protected set; }
        public DateTime? Birthday { get; protected set; }
        public double Reputation { get; protected set; }

        public virtual CustomActionResult ValidateOtp(int otpCode)
        {
            var result = Otp.ValidateOtp(otpCode);
            UpdatedAt = DateTime.Now;

            return result;
        }

        public CustomActionResult GenerateNewOtp()
        {
            var result = Otp.GenerateNewOtp();
            UpdatedAt = DateTime.Now;

            return result;
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
