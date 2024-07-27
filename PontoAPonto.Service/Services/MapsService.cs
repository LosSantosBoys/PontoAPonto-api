using AutoMapper;
using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Enums;
using PontoAPonto.Domain.Interfaces.Infra;
using PontoAPonto.Domain.Interfaces.Rest;
using PontoAPonto.Domain.Interfaces.WebScrapper;
using PontoAPonto.Domain.Models;
using PontoAPonto.Domain.Models.Maps;

public class MapsService : IMapsService
{
    private readonly IMapsApi _mapsApi;
    private readonly IMapper _mapper;
    private readonly IGasPriceScrapper _gasPriceScrapper;
    private readonly IRedisService _redisService;
    private readonly double _proximityThreshold;

    public MapsService(IMapsApi mapsApi,
        IMapper mapper,
        IGasPriceScrapper gasPriceScrapper,
        IRedisService redisService,
        double proximityThreshold = 2000) //TODO - PARAMETRIZE FOR USER VALUE
    {
        _mapsApi = mapsApi;
        _mapper = mapper;
        _gasPriceScrapper = gasPriceScrapper;
        _redisService = redisService;
        _proximityThreshold = proximityThreshold;
    }

    public async Task<CustomActionResult<RouteResponse>> GetRouteAsync(GetRouteRequest request)
    {
        var response = await GetDefaultRoutesAsync(request);

        var hybridRoutes = await GetHybridRoutesAsync(request, response);

        CalculateBestRoutes(request, response, hybridRoutes);

        return response;
    }

    private void CalculateBestRoutes(GetRouteRequest request, CustomActionResult<RouteResponse> response, CustomActionResult<Route>[] hybridRoutes)
    {
        foreach (var hybridRoute in hybridRoutes)
        {
            response.Value.HybridRoutes.Add(hybridRoute.Value);

            if (hybridRoute.Value.Duration < response.Value.FasterRoute.Value.Duration)
            {
                response.Value.FasterRoute = hybridRoute.Value;
            }

            if (hybridRoute.Value.Cost < response.Value.CheapestRoute.Value.Cost)
            {
                response.Value.CheapestRoute = hybridRoute.Value;
            }
        }

        response.Value.RecommendedRoute = GetRecommendedRoute(response.Value, request.UserRoutePreference);
    }

    private async Task<CustomActionResult<Route>[]> GetHybridRoutesAsync(GetRouteRequest request, CustomActionResult<RouteResponse> response)
    {
        var pointsOfInterest = GetPointsOfInterestAsync();

        var hybridRoutesTasks = pointsOfInterest
            .Where(point => IsPointNearRoute(response.Value.OriginalRoute, point))
            .Select(point => CheckRoutePointOfInterestAsync(new Coordinate(request.StartLatitude, request.StartLongitude),
            new Coordinate(request.DestinationLatitude, request.DestinationLongitude),
            point))
            .ToList();

        var hybridRoutes = await Task.WhenAll(hybridRoutesTasks);
        return hybridRoutes;
    }

    private async Task<CustomActionResult<RouteResponse>> GetDefaultRoutesAsync(GetRouteRequest request)
    {
        var start = new Coordinate(request.StartLatitude, request.StartLongitude);
        var dest = new Coordinate(request.DestinationLatitude, request.DestinationLongitude);
        var originalRoute = await GetGoogleRouteAsync(start, dest, request.RouteMode);

        if (!originalRoute.Success)
        {
            return originalRoute.Error;
        }

        originalRoute.Value.Cost = await CalculateRouteCost(originalRoute.Value);

        return new RouteResponse(originalRoute);
    }

    private Route GetRecommendedRoute(RouteResponse response, UserRoutePreference preference)
    {
        switch (preference)
        {
            case UserRoutePreference.CHEAPEST:
                return response.CheapestRoute.Success ? response.CheapestRoute.Value : response.OriginalRoute.Value;
            case UserRoutePreference.FASTER:
                return response.FasterRoute.Success ? response.FasterRoute.Value : response.OriginalRoute.Value;
            case UserRoutePreference.HYBRID:
                return response.HybridRoutes
                    .Where(r => r.Success)
                    .OrderBy(r => r.Value.Duration + r.Value.Cost)
                    .FirstOrDefault()?.Value ?? response.OriginalRoute.Value;
            case UserRoutePreference.ECOLOGIC:
                return response.HybridRoutes
                    .Where(r => r.Success)
                    .OrderBy(r => r.Value.Duration)
                    .FirstOrDefault()?.Value ?? response.OriginalRoute.Value;
            case UserRoutePreference.COMFIEST:
                return response.HybridRoutes
                    .Where(r => r.Success)
                    .OrderBy(r => r.Value.Duration)
                    .FirstOrDefault()?.Value ?? response.OriginalRoute.Value;
            default:
                return response.OriginalRoute.Value;
        }
    }


    private async Task<CustomActionResult<Route>> CheckRoutePointOfInterestAsync(Coordinate start, Coordinate dest, PointOfInterest point)
    {
        var startToPoint = await GetGoogleRouteAsync(start, point, point.Mode);

        if (!startToPoint.Success)
        {
            return startToPoint.Error;
        }

        var pointToDest = await GetGoogleRouteAsync(point, dest, point.Mode);

        if (!pointToDest.Success)
        {
            return pointToDest.Error;
        }

        var hybridRoute = MergeRoutes(startToPoint.Value, pointToDest.Value);
        hybridRoute.Cost = await CalculateRouteCost(hybridRoute);

        return hybridRoute;
    }

    private async Task<decimal> CalculateRouteCost(Route route)
    {
        decimal.TryParse(await _gasPriceScrapper.GetGasolinePriceAsync(), out decimal gasPrice);

        const decimal costPerPublicTransport = 5.50m; // WEBSCRAPPER FOR BUS/METRO PRICE?
        const decimal kmPerLiter = 10m;

        decimal drivingDistance = 0m;
        decimal publicTransportCost = 0m;

        foreach (var leg in route.Legs)
        {
            foreach (var step in leg.Steps)
            {

                if (step.Mode == RouteMode.DRIVING)
                {
                    drivingDistance += step.MetersDistance / 1000.0m;
                }
                else if (step.Mode == RouteMode.TRANSIT)
                {
                    publicTransportCost += costPerPublicTransport;
                }
            }
        }

        decimal fuelConsumed = drivingDistance / kmPerLiter;
        decimal fuelCost = fuelConsumed * gasPrice;

        return fuelCost + publicTransportCost;
    }

    private Route MergeRoutes(Route leg1, Route leg2)
    {
        var mergedRoute = new Route
        {
            Legs = new List<Leg>()
        };

        mergedRoute.Legs.AddRange(leg1.Legs);
        mergedRoute.Legs.AddRange(leg2.Legs);

        mergedRoute.Duration = leg1.Duration + leg2.Duration;

        return mergedRoute;
    }

    private async Task<CustomActionResult<Route>> GetGoogleRouteAsync(Coordinate start, Coordinate destination, RouteMode mode)
    {
        var response = new Route();
        var cacheKey = $"{start.Latitude}-{start.Longitude}-{destination.Latitude}-{destination.Longitude}-{mode}";

        var cacheExist = await _redisService.ExistsAsync(cacheKey);

        if (cacheExist)
        {
            response = await _redisService.GetAsync<Route>(cacheKey);

            if (response != null)
            {
                return response;
            }
        }

        var route = await _mapsApi.GetRouteAsync(start, destination, mode.ToGoogleMapsString());

        if (!route.Success) 
        {
            return route.Error;
        }

        response = _mapper.Map<Route>(route.Value);

        await _redisService.SetAsync(cacheKey, response, TimeSpan.FromMinutes(30));

        return response;
    }

    private IEnumerable<PointOfInterest> GetPointsOfInterestAsync()
    {
        //TODO - DATABASE FOR EACH USER DEFINE THEIR POINT OF INTEREST + GLOBAL POINTS OF INTERESTS 
        return new List<PointOfInterest>
        {
            new PointOfInterest { Name = "Estação de Metrô Tamanduatei", Coordinate = new Coordinate(-23.592741196050046, -46.58945103397126), Mode = RouteMode.TRANSIT },
        };
    }

    private bool IsPointNearRoute(Route route, PointOfInterest point)
    {
        foreach (var leg in route.Legs)
        {
            foreach (var step in leg.Steps)
            {
                if (CalculateDistance(step.EndLocation, point.Coordinate) <= _proximityThreshold)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private double CalculateDistance(Coordinate coord1, Coordinate coord2)
    {
        double dLat = (coord2.Latitude - coord1.Latitude) * Math.PI / 180.0;
        double dLon = (coord2.Longitude - coord1.Longitude) * Math.PI / 180.0;
        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(coord1.Latitude * Math.PI / 180.0) * Math.Cos(coord2.Latitude * Math.PI / 180.0) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        double distance = 6371.0 * c;
        double responseInM = distance * 1000;
        return responseInM;
    }
}