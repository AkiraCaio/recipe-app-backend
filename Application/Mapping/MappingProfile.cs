using AutoMapper;
using RecipeAppBackend.Application.DTO;
using RecipeAppBackend.Domain.Entities;

namespace RecipeAppBackend.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserCreateDto, User>();
        }
    }
}