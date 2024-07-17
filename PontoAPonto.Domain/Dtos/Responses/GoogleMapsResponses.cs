using Newtonsoft.Json;

namespace PontoAPonto.Domain.Dtos.Responses
{
    public class GoogleMapsResponse
    {
        [JsonProperty("routes")]
        public List<GoogleMapsRoute> Routes { get; set; }
    }

    public class GoogleMapsRoute
    {
        [JsonProperty("legs")]
        public List<GoogleMapsLeg> Legs { get; set; }
    }

    public class GoogleMapsLeg
    {
        [JsonProperty("steps")]
        public List<GoogleMapsStep> Steps { get; set; }

        [JsonProperty("duration")]
        public GoogleMapsDuration Duration { get; set; }
    }

    public class GoogleMapsStep
    {
        [JsonProperty("start_location")]
        public GoogleMapsLocation StartLocation { get; set; }

        [JsonProperty("end_location")]
        public GoogleMapsLocation EndLocation { get; set; }

        [JsonProperty("distance")]
        public GoogleMapsDistance Distance { get; set; }

        [JsonProperty("travel_mode")]
        public string TravelMode { get; set; }
    }

    public class GoogleMapsDistance
    {
        [JsonProperty("value")]
        public int Value { get; set; }
    }

    public class GoogleMapsLocation
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }
    }

    public class GoogleMapsDuration
    {
        [JsonProperty("value")]
        public int Value { get; set; }
    }
}
