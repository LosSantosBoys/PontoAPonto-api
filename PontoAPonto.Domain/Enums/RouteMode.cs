namespace PontoAPonto.Domain.Enums
{
    public enum RouteMode
    {
        DRIVING,
        WALKING,
        TRANSIT
    }

    public static class RouteModesExtensions
    {
        public static string ToGoogleMapsString(this RouteMode mode)
        {
            switch (mode)
            {
                case RouteMode.DRIVING:
                    return "driving";
                case RouteMode.WALKING:
                    return "walking";
                case RouteMode.TRANSIT:
                    return "transit";
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        public static RouteMode ToEnum(string mode)
        {
            switch (mode.ToLower())
            {
                case "driving":
                    return RouteMode.DRIVING;
                case "walking":
                    return RouteMode.WALKING;
                case "transit":
                    return RouteMode.TRANSIT;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}
