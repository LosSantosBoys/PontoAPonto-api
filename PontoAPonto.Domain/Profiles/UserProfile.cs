using AutoMapper;
using PontoAPonto.Domain.Dtos.Responses.User;
using PontoAPonto.Domain.Models.Entities;

namespace PontoAPonto.Domain.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserProfileResponse>()
                .ForMember(src => src.UserSince, opt => opt.MapFrom(u => u.CreatedAt));
        }
    }
}
