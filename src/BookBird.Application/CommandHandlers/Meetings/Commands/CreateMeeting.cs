using System;
using BookBird.Domain.Enumerations;
using MediatR;

namespace BookBird.Application.CommandHandlers.Meetings.Commands
{
    public sealed record CreateMeeting(
        string Name,
        string Location,
        DateTime ScheduledFor,
        MeetingType Type,
        int? MaxNumberOfVisitors,
        Guid OwnerId) : IRequest<Guid>;
}