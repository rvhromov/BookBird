using System;
using BookBird.Application.DTOs.Authors;
using MediatR;

namespace BookBird.Infrastructure.QueryHandlers.Authors.Queries
{
    public sealed record GetAuthor(Guid Id) : IRequest<AuthorWithBooksDto>;
}