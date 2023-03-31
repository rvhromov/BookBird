using BookBird.Application.DTOs.Books;
using BookBird.Application.Helpers;
using MediatR;

namespace BookBird.Infrastructure.QueryHandlers.Books.Queries
{
    public sealed record GetBooks(int Skip, int Take, string Name, string AuthorName) : IRequest<IPaginatedList<BookWithAuthorDto>>;
}