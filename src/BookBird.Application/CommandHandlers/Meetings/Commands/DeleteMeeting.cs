using System;
using MediatR;

namespace BookBird.Application.CommandHandlers.Meetings.Commands
{
    public sealed record DeleteMeeting(Guid Id) : IRequest;
}