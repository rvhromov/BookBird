using BookBird.Application.DTOs.Genres;
using BookBird.Application.Helpers;
using MediatR;

namespace BookBird.Infrastructure.QueryHandlers.Genres.Queries
{
    public sealed record GetGenres(int Skip, int Take, string Name) : IRequest<IPaginatedList<GenreDto>>;
}