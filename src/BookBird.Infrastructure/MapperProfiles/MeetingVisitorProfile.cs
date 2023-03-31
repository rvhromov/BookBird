using AutoMapper;
using BookBird.Application.DTOs.Meetings;
using BookBird.Application.DTOs.MeetingVisitors;
using BookBird.Infrastructure.EF.Models;

namespace BookBird.Infrastructure.MapperProfiles
{
    public sealed class MeetingVisitorProfile : Profile
    {
        public MeetingVisitorProfile()
        {
            CreateMap<MeetingVisitorReadModel, MeetingVisitorDto>(MemberList.None)
                .ForMember(dst => dst.MeetingName, opt => opt.MapFrom(src => src.Meeting.Name))
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dst => dst.UserEmail, opt => opt.MapFrom(src => src.User.Email));

            CreateMap<MeetingReadModel, MeetingBaseDto>(MemberList.None);
        }
    }
}