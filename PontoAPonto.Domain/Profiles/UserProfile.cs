using AutoMapper;
using PontoAPonto.Domain.Dtos.Responses;
using PontoAPonto.Domain.Models.Entities;

namespace PontoAPonto.Domain.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserResponse>();
        }
    }
}
