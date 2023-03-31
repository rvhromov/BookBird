using System.Linq;
using AutoMapper;
using BookBird.Application.DTOs.Books;
using BookBird.Application.DTOs.BookSearch;
using BookBird.Domain.Enums;
using BookBird.Infrastructure.EF.Models;

namespace BookBird.Infrastructure.MapperProfiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookReadModel, BookDto>(MemberList.None);
            CreateMap<BookReadModel, BookWithAuthorDto>(MemberList.None)
                .ForMember(dst => dst.Author, opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"));

            CreateMap<BookReadModel, BookExtendedDto>(MemberList.None)
                .ForMember(dst => dst.AuthorId, opt => opt.MapFrom(src => src.Author.Id))
                .ForMember(dst => dst.AuthorFirstName, opt => opt.MapFrom(src => src.Author.FirstName))
                .ForMember(dst => dst.AuthorLastName, opt => opt.MapFrom(src => src.Author.LastName))
                .ForMember(dst => dst.Genres, opt => opt.MapFrom(src => src.Genres.Where(g => g.Status == Status.Active)));

            CreateMap<BookReadModel, BookIndexDto>(MemberList.None);
        }
    }
}