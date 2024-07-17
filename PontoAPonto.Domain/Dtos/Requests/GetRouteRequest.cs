using PontoAPonto.Domain.Enums;
using PontoAPonto.Domain.Models.Maps;
using System.ComponentModel.DataAnnotations;

namespace PontoAPonto.Domain.Dtos.Requests
{
    public class GetRouteRequest
    {
        [Required]
        public Coordinate StartCoordinate { get; set; }
        [Required]
        public Coordinate DestinationCoordinate {  get; set; }
        [Required]
        public RouteMode RouteMode { get; set; }
    }
}
