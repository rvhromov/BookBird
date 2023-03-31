using AutoMapper;
using BookBird.Application.DTOs.Feedbacks;
using BookBird.Infrastructure.EF.Models;

namespace BookBird.Infrastructure.MapperProfiles
{
    public sealed class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {
            CreateMap<FeedbackReadModel, FeedbackDto>(MemberList.None);
        }
    }
}