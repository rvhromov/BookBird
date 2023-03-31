using System;
using MediatR;

namespace BookBird.Application.CommandHandlers.Genres.Commands
{
    public sealed record CreateGenre(string Name, string Description) : IRequest<Guid>;
}