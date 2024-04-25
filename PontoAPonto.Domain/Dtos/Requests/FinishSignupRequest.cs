using System.ComponentModel.DataAnnotations;

namespace PontoAPonto.Domain.Dtos.Requests
{
    public class FinishSignupRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Cpf { get; set; }

        [Required]
        public DateTime Birthday { get; set; }
    }
}
