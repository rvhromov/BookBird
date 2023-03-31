using System;
using System.Text.Json.Serialization;
using MediatR;

namespace BookBird.Application.CommandHandlers.Feedbacks.Commands
{
    public sealed record UpdateFeedback(string Description, ushort Rating) : IRequest
    {
        [JsonIgnore]
        public Guid Id { get; init; }
    }
}