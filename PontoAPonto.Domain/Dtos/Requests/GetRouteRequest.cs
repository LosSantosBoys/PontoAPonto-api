using PontoAPonto.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PontoAPonto.Domain.Dtos.Requests
{
    public class GetRouteRequest
    {
        [Required]
        public double StartLatitude { get; set; }

        [Required]
        public double StartLongitude { get; set; }

        [Required]
        public double DestinationLatitude { get; set; }

        [Required]
        public double DestinationLongitude { get; set; }

        [Required]
        public RouteMode RouteMode { get; set; }

        [Required]
        public UserRoutePreference UserRoutePreference { get; set; }
    }
}
