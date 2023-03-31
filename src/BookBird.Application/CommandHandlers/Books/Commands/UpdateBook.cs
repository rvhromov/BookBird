using System;
using System.Collections.Generic;
using MediatR;

namespace BookBird.Application.CommandHandlers.Books.Commands
{
    public sealed record UpdateBook(string Name, ushort PublishYear, Guid AuthorId, IEnumerable<Guid> GenreIds) : IRequest
    {
        public Guid Id { get; init; }
    }
}