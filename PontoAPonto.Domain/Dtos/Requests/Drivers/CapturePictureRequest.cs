using System.ComponentModel.DataAnnotations;

namespace PontoAPonto.Domain.Dtos.Requests.Drivers
{
    public class CapturePictureRequest
    {
        [Required]
        public string ImageBase64 { get; set; }
    }
}
