using AutoMapper;
using BookBird.Application.DTOs.Invitations;
using BookBird.Infrastructure.EF.Models;

namespace BookBird.Infrastructure.MapperProfiles
{
    public sealed class InvitationProfile : Profile
    {
        public InvitationProfile()
        {
            CreateMap<InvitationReadModel, InvitationDto>(MemberList.None)
                .ForMember(dst => dst.MeetingName, opt => opt.MapFrom(src => src.Meeting.Name))
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dst => dst.UserEmail, opt => opt.MapFrom(src => src.User.Email));
        }
    }
}