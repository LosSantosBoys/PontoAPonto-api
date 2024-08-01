namespace PontoAPonto.Domain.Models
{
    public class Location
    {
        public Location(string country, string state, string city)
        {
            Country = country;
            State = state;
            City = city;
        }

        public string Country { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; }

        public void UpdateLocation(string? country, string? state, string? city)
        {
            if (!string.IsNullOrEmpty(country))
            {
                Country = country;
            }

            if (!string.IsNullOrEmpty(state))
            {
                State = state;
            }

            if (!string.IsNullOrEmpty(city))
            {
                City = city;
            }
        }
    }
}
