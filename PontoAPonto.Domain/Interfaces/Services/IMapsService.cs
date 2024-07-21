using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Models;

public interface IMapsService
{
    Task<CustomActionResult<RouteResponse>> GetRouteAsync(GetRouteRequest request);
}