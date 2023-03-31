using System;
using System.Text.Json.Serialization;
using BookBird.Application.DTOs.Meetings;
using BookBird.Application.Helpers;
using MediatR;

namespace BookBird.Infrastructure.QueryHandlers.Meetings.Queries
{
    public sealed record GetOwnMeetings(int Skip, int Take) : IRequest<IPaginatedList<MeetingBaseDto>>
    {
        [JsonIgnore]
        public Guid UserId { get; init; }
    }
}