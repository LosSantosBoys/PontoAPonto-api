using PontoAPonto.Domain.Models;

namespace PontoAPonto.Domain.Dtos.Requests.Drivers
{
    public class ChangeProfileRequest
    {
        public string? ProfilePictureBase64 { get; set; }
        public CarInfo? CarInfo { get; set; }
        public Location? Location { get; set; }
    }
}