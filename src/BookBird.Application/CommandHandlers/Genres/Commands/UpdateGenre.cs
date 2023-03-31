using System;
using System.Text.Json.Serialization;
using MediatR;

namespace BookBird.Application.CommandHandlers.Genres.Commands
{
    public sealed record UpdateGenre(string Name, string Description) : IRequest
    {
        [JsonIgnore]
        public Guid Id { get; init; }
    }
}