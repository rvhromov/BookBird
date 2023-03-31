using BookBird.Application.DTOs.Authors;
using BookBird.Application.Helpers;
using MediatR;

namespace BookBird.Infrastructure.QueryHandlers.Authors.Queries
{
    public sealed record GetAuthors(int Skip, int Take, string Name) : IRequest<IPaginatedList<AuthorDto>>;
}