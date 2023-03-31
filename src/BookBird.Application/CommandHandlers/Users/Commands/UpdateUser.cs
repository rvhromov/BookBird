using System;
using System.Text.Json.Serialization;
using MediatR;

namespace BookBird.Application.CommandHandlers.Users.Commands
{
    public sealed record UpdateUser(string Name, string Email) : IRequest
    {
        [JsonIgnore]
        public Guid Id { get; init; }
    }
}