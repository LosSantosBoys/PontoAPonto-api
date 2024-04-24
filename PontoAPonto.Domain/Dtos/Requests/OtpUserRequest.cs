using PontoAPonto.Domain.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace PontoAPonto.Domain.Dtos.Requests
{
    public class OtpUserRequest
    {
        public OtpUserRequest(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone {  get; set; }

        public User ToEntity()
        {
            return new User().CreateUser(Name, Email, Phone);
        }
    }
}
