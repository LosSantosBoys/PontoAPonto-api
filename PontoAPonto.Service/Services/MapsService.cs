using AutoMapper;
using PontoAPonto.Domain.Dtos.Requests;
using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Enums;
using PontoAPonto.Domain.Interfaces.Rest;
using PontoAPonto.Domain.Interfaces.WebScrapper;
using PontoAPonto.Domain.Models.Maps;

public class MapsService : IMapsService
{
    private readonly IMapsApi _mapsApi;
    private readonly IMapper _mapper;
    private readonly IGasPriceScrapper _gasPriceScrapper;
    private readonly double _proximityThreshold;

    public MapsService(IMapsApi mapsApi, 
        IMapper mapper, 
        IGasPriceScrapper gasPriceScrapper, 
        double proximityThreshold = 2000) //TODO - PARAMETRIZE FOR USER VALUE
    {
        _mapsApi = mapsApi;
        _mapper = mapper;
        _gasPriceScrapper = gasPriceScrapper;
        _proximityThreshold = proximityThreshold;
    }

    public async Task<RouteResponse> GetRouteAsync(GetRouteRequest request)
    {
        var start = new Coordinate(request.StartLatitude, request.StartLongitude);
        var dest = new Coordinate(request.DestinationLatitude, request.DestinationLongitude);

        var originalRoute = await GetGoogleRouteAsync(start, dest, request.RouteMode);
        originalRoute.Cost = await CalculateRouteCost(originalRoute);

        var response = new RouteResponse(originalRoute);

        var pointsOfInterest = GetPointsOfInterestAsync();

        var hybridRoutesTasks = pointsOfInterest
            .Where(point => IsPointNearRoute(originalRoute, point))
            .Select(point => CheckRoutePointOfInterestAsync(start, dest, point))
            .ToList();

        var hybridRoutes = await Task.WhenAll(hybridRoutesTasks);

        foreach (var hybridRoute in hybridRoutes)
        {
            response.HybridRoutes.Add(hybridRoute);

            if (hybridRoute.Duration < response.FasterRoute.Duration)
            {
                response.FasterRoute = hybridRoute;
            }

            if (hybridRoute.Cost < response.CheapestRoute.Cost)
            {
                response.CheapestRoute = hybridRoute;
            }
        }

        response.RecommendedRoute = GetRecommendedRoute(response, request.UserRoutePreference);

        return response;
    }

    private Route GetRecommendedRoute(RouteResponse response, UserRoutePreference preference)
    {
        switch (preference)
        {
            case UserRoutePreference.CHEAPEST:
                return response.CheapestRoute;
            case UserRoutePreference.FASTER:
                return response.FasterRoute;
            case UserRoutePreference.HYBRID:
                return response.HybridRoutes.OrderBy(r => r.Duration + r.Cost).FirstOrDefault() ?? response.OriginalRoute;
            case UserRoutePreference.ECOLOGIC:
                return response.HybridRoutes.OrderBy(r => r.Duration).FirstOrDefault() ?? response.OriginalRoute; //TODO - DEFINE
            case UserRoutePreference.COMFIEST:
                return response.HybridRoutes.OrderBy(r => r.Duration).FirstOrDefault() ?? response.OriginalRoute; //TODO - DEFINE
            default:
                return response.OriginalRoute;
        }
    }

    private async Task<Route> CheckRoutePointOfInterestAsync(Coordinate start, Coordinate dest, PointOfInterest point)
    {
        var startToPoint = await GetGoogleRouteAsync(start, point, point.Mode);
        var pointToDest = await GetGoogleRouteAsync(point, dest, point.Mode);

        var hybridRoute = MergeRoutes(startToPoint, pointToDest);
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

    private async Task<Route> GetGoogleRouteAsync(Coordinate start, Coordinate destination, RouteMode mode)
    {
        var route = await _mapsApi.GetRouteAsync(start, destination, mode.ToGoogleMapsString());
        return _mapper.Map<Route>(route);
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