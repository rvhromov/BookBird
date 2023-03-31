using System;
using System.Text.Json.Serialization;
using MediatR;

namespace BookBird.Application.CommandHandlers.Authors.Commands
{
    public sealed record UpdateAuthor(string FirstName, string LastName) : IRequest
    {
        [JsonIgnore]
        public Guid Id { get; init; }
    }
}