using PontoAPonto.Domain.Models;
using PontoAPonto.Domain.Models.Maps;

namespace PontoAPonto.Domain.Dtos.Responses
{
    public class RouteResponse
    {
        public RouteResponse(Route defaultRoute)
        {
            OriginalRoute = defaultRoute;
            HybridRoutes = [];
            FasterRoute = defaultRoute;
            CheapestRoute = defaultRoute;
            RecommendedRoute = defaultRoute;
        }

        public CustomActionResult<Route> OriginalRoute { get; set; }
        public List<CustomActionResult<Route>> HybridRoutes { get; set; }
        public CustomActionResult<Route> FasterRoute { get; set; }
        public CustomActionResult<Route> CheapestRoute { get; set; }
        public CustomActionResult<Route> RecommendedRoute { get; set; }
    }
}