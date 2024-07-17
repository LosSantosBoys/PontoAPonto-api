using PontoAPonto.Domain.Enums;

namespace PontoAPonto.Domain.Models.Maps
{
    public class Route
    {
        public decimal Cost { get; set; }
        public int Duration { get; set; }
        public List<Leg> Legs { get; set; }
    }

    public class Leg
    {
        public int Duration { get; set; }
        public List<Step> Steps { get; set; }
    }

    public class Step
    {
        public int MetersDistance { get; set; }
        public RouteMode Mode { get; set; }
        public Coordinate StartLocation { get; set; }
        public Coordinate EndLocation { get; set; }
    }

    public class Coordinate
    {
        public Coordinate()
        {

        }

        public Coordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class PointOfInterest
    {
        public string Name { get; set; }
        public Coordinate Coordinate { get; set; }
        public RouteMode Mode { get; set; }

        public static implicit operator Coordinate(PointOfInterest pointOfInterest) 
        {
            return pointOfInterest.Coordinate;
        }
    }
}
