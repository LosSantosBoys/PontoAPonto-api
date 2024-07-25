using Newtonsoft.Json;
using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Errors;
using PontoAPonto.Domain.Interfaces.Rest;
using PontoAPonto.Domain.Models;
using PontoAPonto.Domain.Models.Configs;
using PontoAPonto.Domain.Models.Maps;
using System.Globalization;

namespace PontoAPonto.Data.Rest
{
    public class MapsApi : IMapsApi
    {
        private readonly HttpClient _httpClient;
        private readonly ApiKeys _apiKeys;

        public MapsApi(HttpClient httpClient, ApiKeys apiKeys)
        {
            _httpClient = httpClient;
            _apiKeys = apiKeys;
        }

        public async Task<CustomActionResult<GoogleMapsResponse>> GetRouteAsync(Coordinate start, Coordinate destination, string mode)
        {
            var startLat = ParseCoordinateToString(start.Latitude);
            var startLng = ParseCoordinateToString(start.Longitude);
            var destLat = ParseCoordinateToString(destination.Latitude);
            var destLng = ParseCoordinateToString(destination.Longitude);

            var url = $"https://maps.googleapis.com/maps/api/directions/json?origin={startLat},{startLng}&destination={destLat},{destLng}&mode={mode}&key={_apiKeys.Maps}";
            var response = await _httpClient.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return MapsError.HttpError(response.StatusCode, content);
            }

            var route = JsonConvert.DeserializeObject<GoogleMapsResponse>(content);

            return route;
        }

        private string ParseCoordinateToString(double coordinate)
        {
            // If the coordinate isn't parsed by this method,
            // the pt-BR culture separate decimal with "," instead of ".", causing error on google url
            return coordinate.ToString(CultureInfo.InvariantCulture);
        }
    }
}
