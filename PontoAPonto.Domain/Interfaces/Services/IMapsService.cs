using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Enums;
using PontoAPonto.Domain.Models.Maps;

public interface IMapsService
{
    Task<RouteResponse> GetRouteAsync(Coordinate start, Coordinate destination, RouteMode mode);
}