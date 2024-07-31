using PontoAPonto.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PontoAPonto.Domain.Dtos.Requests.SignUp
{
    public class ValidateOtpRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public int Otp { get; set; }

        [Required]
        public UserType UserType { get; set; }
    }
}
