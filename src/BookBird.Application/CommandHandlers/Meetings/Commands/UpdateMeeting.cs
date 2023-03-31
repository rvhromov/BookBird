using System;
using System.Text.Json.Serialization;
using MediatR;

namespace BookBird.Application.CommandHandlers.Meetings.Commands
{
    public sealed record UpdateMeeting(string Name, string Location, DateTime ScheduledFor, int? NumberOfVisitors) : IRequest
    {
        [JsonIgnore]
        public Guid Id { get; init; }
    }
}