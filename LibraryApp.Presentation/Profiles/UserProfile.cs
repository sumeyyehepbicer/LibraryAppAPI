using AutoMapper;
using LibraryApp.Domain.Entities;
using LibraryApp.Shared.DTOs.UserDtos;

namespace LibraryApp.Presentation.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
