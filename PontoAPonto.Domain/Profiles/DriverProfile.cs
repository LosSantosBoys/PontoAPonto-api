using AutoMapper;
using PontoAPonto.Domain.Dtos.Responses.Driver;
using PontoAPonto.Domain.Models.Entities;

namespace PontoAPonto.Domain.Profiles
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<Driver, DriverProfileResponse>()
                .ForMember(src => src.DriverSince, dest => dest.MapFrom(x => x.ApprovedAt));
        }
    }
}
