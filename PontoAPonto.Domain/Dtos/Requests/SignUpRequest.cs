using PontoAPonto.Domain.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace PontoAPonto.Domain.Dtos.Requests
{
    public class SignUpRequest
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(13)]
        public string Phone {  get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [MaxLength(11)]
        public string Cpf { get; set; }

        [Required]
        [MaxLength(12)]
        public string Birthday { get; set; }

        public User ToEntity(byte[] passwordHash, byte[] passwordSalt, DateTime birthdayDate)
        {
            return new User().CreateUser(Name, Email, Phone, passwordHash, passwordSalt, Cpf, birthdayDate);
        }
    }
}
