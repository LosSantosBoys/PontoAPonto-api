using System.ComponentModel.DataAnnotations;

namespace PontoAPonto.Domain.Dtos.Requests
{
    public class GenerateNewOtpRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
