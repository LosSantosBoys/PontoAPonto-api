using PontoAPonto.Domain.Models;

namespace PontoAPonto.Domain.Dtos.Responses.Driver
{
    public class DriverProfileResponse
    {
        public string ProfilePictureUrl { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }
        public double Reputation { get; set; }
        public DateTime DriverSince { get; set; }
        public CarInfo CarInfo { get; set; }
    }
}
