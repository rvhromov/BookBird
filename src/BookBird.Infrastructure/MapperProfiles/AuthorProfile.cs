using AutoMapper;
using BookBird.Application.DTOs.Authors;
using BookBird.Application.DTOs.BookSearch;
using BookBird.Infrastructure.EF.Models;

namespace BookBird.Infrastructure.MapperProfiles
{
    public sealed class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorReadModel, AuthorDto>(MemberList.None);
            CreateMap<AuthorReadModel, AuthorWithBooksDto>(MemberList.None);
            CreateMap<AuthorReadModel, AuthorIndexDto>(MemberList.None);
        }
    }
}