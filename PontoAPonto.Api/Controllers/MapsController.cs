using Microsoft.AspNetCore.Mvc;
using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Models;
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
        public async Task<CustomActionResult<RouteResponse>> GetRoute([FromQuery] GetRouteRequest request)
        {
            return await _mapsService.GetRouteAsync(request);
        }
    }
}
