using PontoAPonto.Domain.Models.Maps;

namespace PontoAPonto.Domain.Dtos.Responses
{
    public class RouteResponse
    {
        public Route OriginalRoute { get; set; }
        public Route HybridRoute { get; set; }
        public Route FasterRoute { get; set; }
        public Route CheapestRoute { get; set; }
        public Route RecommendRoute { get; set; }
    }
}
