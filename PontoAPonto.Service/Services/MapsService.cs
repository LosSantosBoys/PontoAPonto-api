using AutoMapper;
using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Enums;
using PontoAPonto.Domain.Interfaces.Rest;
using PontoAPonto.Domain.Models.Maps;

public class MapsService : IMapsService
{
    private readonly IMapsApi _mapsApi;
    private readonly IMapper _mapper;
    private readonly double _proximityThreshold;

    public MapsService(IMapsApi mapsApi, IMapper mapper, double proximityThreshold = 2000) //TODO - PARAMETRIZE FOR USER VALUE
    {
        _mapsApi = mapsApi;
        _mapper = mapper;
        _proximityThreshold = proximityThreshold;
    }

    public async Task<RouteResponse> GetRouteAsync(Coordinate start, Coordinate destination, RouteMode mode)
    {
        var response = new RouteResponse();
        var originalRoute = await _mapsApi.GetRouteAsync(start, destination, mode);

        response.OriginalRoute = _mapper.Map<Route>(originalRoute);

        var pointsOfInterest = GetPointsOfInterestAsync();

        foreach (var point in pointsOfInterest)
        {
            if (IsPointNearRoute(originalRoute, point))
            {
                var pointCoordinate = new Coordinate(point.Coordinate.Latitude, point.Coordinate.Longitude);

                var startToPoint = await _mapsApi.GetRouteAsync(start, pointCoordinate, mode);

                var routeStartToPoint = _mapper.Map<Route>(startToPoint);

                var pointToDest = await _mapsApi.GetRouteAsync(pointCoordinate, destination, point.Mode);

                var routePointToDest = _mapper.Map<Route>(pointToDest);

                var totalTime = routeStartToPoint.Duration + routePointToDest.Duration;
                
                if (totalTime < originalRoute.Duration)
                {
                    response.FasterRoute = routeStartToPoint;
                }
            }
        }

        return response;
    }

    private IEnumerable<PointOfInterest> GetPointsOfInterestAsync()
    {
        //TODO - DATABASE FOR EACH USER DEFINE THEIR POINT OF INTEREST + GLOBAL POINTS OF INTERESTS 
        return new List<PointOfInterest>
        {
            new PointOfInterest { Name = "Estação de Metrô Tamanduatei", Coordinate = new Coordinate(-23.592741196050046, -46.58945103397126), Mode = "transit" },
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