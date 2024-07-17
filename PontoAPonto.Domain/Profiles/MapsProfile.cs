using AutoMapper;
using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Models.Maps;

namespace PontoAPonto.Domain.Profiles
{
    public class MapsProfile : Profile
    {
        public MapsProfile()
        {
            CreateMap<GoogleMapsResponse, Route>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Routes.FirstOrDefault().Legs.Sum(leg => leg.Duration.Value)))
                .ForMember(dest => dest.Legs, opt => opt.MapFrom(src => src.Routes.FirstOrDefault().Legs));

            CreateMap<GoogleMapsLeg, Leg>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration.Value))
                .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.Steps));

            CreateMap<GoogleMapsStep, Step>()
                .ForMember(dest => dest.StartLocation, opt => opt.MapFrom(src => src.StartLocation))
                .ForMember(dest => dest.EndLocation, opt => opt.MapFrom(src => src.EndLocation));

            CreateMap<GoogleMapsLocation, Coordinate>()
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Lat))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Lng));
        }
    }
}
