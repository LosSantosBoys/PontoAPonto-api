using System.ComponentModel.DataAnnotations;

namespace PontoAPonto.Domain.Dtos.Requests.Drivers
{
    public class CaptureProfilePictureRequest
    {
        [Required]
        public string ImageBase64 { get; set; }
    }
}
