using System.ComponentModel.DataAnnotations;

namespace PontoAPonto.Domain.Dtos.Requests
{
    public class ValidateOtpRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Otp {  get; set; }
    }
}
