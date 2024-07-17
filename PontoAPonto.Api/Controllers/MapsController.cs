using Microsoft.AspNetCore.Mvc;
using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Models.Maps;

namespace PontoAPonto.Api.Controllers
{
    [ApiController]
    [Route("api/v1/route")]
    public class MapsController : ControllerBase
    {
        private readonly IMapsService _mapsService;

        public MapsController(IMapsService mapsService)
        {
            _mapsService = mapsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoute([FromBody] GetRouteRequest request)
        {
            var route = await _mapsService.GetRouteAsync(request.StartCoordinate, request.DestinationCoordinate, request.RouteMode);

            return Ok(route);
        }
    }
}
