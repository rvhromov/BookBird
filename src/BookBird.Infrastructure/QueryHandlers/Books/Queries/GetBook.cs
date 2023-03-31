using System;
using BookBird.Application.DTOs.Books;
using MediatR;

namespace BookBird.Infrastructure.QueryHandlers.Books.Queries
{
    public sealed record GetBook(Guid Id) : IRequest<BookExtendedDto>;
}