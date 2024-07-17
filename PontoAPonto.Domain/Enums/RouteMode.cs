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
    }
}
