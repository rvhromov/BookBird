using AutoMapper;
using BookBird.Application.DTOs.Users;
using BookBird.Infrastructure.EF.Models;

namespace BookBird.Infrastructure.MapperProfiles
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserReadModel, UserDto>(MemberList.None);
        }
    }
}