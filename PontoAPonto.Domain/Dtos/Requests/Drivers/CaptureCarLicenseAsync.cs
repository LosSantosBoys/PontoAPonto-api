using System.ComponentModel.DataAnnotations;

namespace PontoAPonto.Domain.Dtos.Requests.Drivers
{
    public class CaptureCarLicenseAsync
    {
        [Required]
        public string PdfBase64 { get; set; }
    }
}
