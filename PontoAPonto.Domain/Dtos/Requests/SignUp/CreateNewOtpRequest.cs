using PontoAPonto.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PontoAPonto.Domain.Dtos.Requests.SignUp
{
    public class CreateNewOtpRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public UserType UserType { get; set; }
    }
}
