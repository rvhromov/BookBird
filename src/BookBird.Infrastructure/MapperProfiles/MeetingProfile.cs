using AutoMapper;
using BookBird.Application.DTOs.Meetings;
using BookBird.Infrastructure.EF.Models;

namespace BookBird.Infrastructure.MapperProfiles
{
    public sealed class MeetingProfile : Profile
    {
        public MeetingProfile()
        {
            CreateMap<MeetingReadModel, MeetingDto>(MemberList.None);
        }
    }
}