using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Models;
using PontoAPonto.Domain.Models.Maps;

namespace PontoAPonto.Domain.Interfaces.Rest
{
    public interface IMapsApi
    {
        Task<CustomActionResult<GoogleMapsResponse>> GetRouteAsync(Coordinate start, Coordinate destination, string mode);
    }
}
