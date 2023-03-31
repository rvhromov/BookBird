using System.Collections.Generic;
using BookBird.Application.DTOs.BookSearch;
using MediatR;

namespace BookBird.Infrastructure.QueryHandlers.BookSearch.Queries
{
    public sealed record SearchBook(int Skip, int Take, string SearchTerm) : IRequest<IReadOnlyCollection<BookIndexDto>>;
}