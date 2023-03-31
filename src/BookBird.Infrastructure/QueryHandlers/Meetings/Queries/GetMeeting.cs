using System;
using BookBird.Application.DTOs.Meetings;
using MediatR;

namespace BookBird.Infrastructure.QueryHandlers.Meetings.Queries
{
    public sealed record GetMeeting(Guid Id) : IRequest<MeetingDto>;
}