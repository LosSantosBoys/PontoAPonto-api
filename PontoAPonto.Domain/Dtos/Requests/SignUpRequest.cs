using PontoAPonto.Domain.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace PontoAPonto.Domain.Dtos.Requests
{
    public class SignUpRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone {  get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Cpf { get; set; }

        [Required]
        public string Birthday { get; set; }

        public User ToEntity(byte[] passwordHash, byte[] passwordSalt)
        {
            if (!DateTime.TryParseExact(Birthday, "ddMMyyyy", null, System.Globalization.DateTimeStyles.None, out DateTime birthdayDate))
            {
                throw new ArgumentException("Invalid birthday format. Please provide the birthday in the format 'ddMMyyyy'.");
            }

            return new User().CreateUser(Name, Email, Phone, passwordHash, passwordSalt, Cpf, birthdayDate);
        }
    }
}
