using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Dtos.Responses;

public interface IMapsService
{
    Task<RouteResponse> GetRouteAsync(GetRouteRequest request);
}