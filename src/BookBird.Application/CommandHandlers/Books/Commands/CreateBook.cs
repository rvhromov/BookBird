using System;
using System.Collections.Generic;
using MediatR;

namespace BookBird.Application.CommandHandlers.Books.Commands
{
    public sealed record CreateBook(string Name, ushort PublishYear, Guid AuthorId, IEnumerable<Guid> GenreIds) : IRequest<Guid>;
}