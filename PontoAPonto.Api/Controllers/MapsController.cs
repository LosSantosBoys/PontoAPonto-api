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
        public async Task<IActionResult> GetRoute([FromQuery] GetRouteRequest request)
        {
            var route = await _mapsService.GetRouteAsync(request);

            return Ok(route);
        }
    }
}
