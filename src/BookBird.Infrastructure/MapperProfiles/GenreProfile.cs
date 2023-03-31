using AutoMapper;
using BookBird.Application.DTOs.BookSearch;
using BookBird.Application.DTOs.Genres;
using BookBird.Infrastructure.EF.Models;

namespace BookBird.Infrastructure.MapperProfiles
{
    public sealed class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<GenreReadModel, GenreDto>(MemberList.None);
            CreateMap<GenreReadModel, GenreIndexDto>(MemberList.None);
        }
    }
}