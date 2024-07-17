using PontoAPonto.Domain.Models.Maps;

namespace PontoAPonto.Domain.Dtos.Responses
{
    public class RouteResponse
    {
        public RouteResponse()
        {
            
        }

        public RouteResponse(Route defaultRoute)
        {
            OriginalRoute = defaultRoute;
            HybridRoutes = [];
            FasterRoute = defaultRoute;
            CheapestRoute = defaultRoute;
            RecommendedRoute = defaultRoute;
        }

        public Route OriginalRoute { get; set; }
        public List<Route> HybridRoutes { get; set; }
        public Route FasterRoute { get; set; }
        public Route CheapestRoute { get; set; }
        public Route RecommendedRoute { get; set; }
    }
}
