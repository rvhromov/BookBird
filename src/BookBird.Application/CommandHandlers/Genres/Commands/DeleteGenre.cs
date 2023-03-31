using System;
using MediatR;

namespace BookBird.Application.CommandHandlers.Genres.Commands
{
    public sealed record DeleteGenre(Guid Id) : IRequest;
}