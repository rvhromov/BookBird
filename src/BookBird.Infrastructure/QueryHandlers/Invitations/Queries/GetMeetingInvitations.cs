using System;
using System.Text.Json.Serialization;
using BookBird.Application.DTOs.Invitations;
using BookBird.Application.Helpers;
using MediatR;

namespace BookBird.Infrastructure.QueryHandlers.Invitations.Queries
{
    public sealed record GetMeetingInvitations(int Skip, int Take) : IRequest<IPaginatedList<InvitationDto>>
    {
        [JsonIgnore]
        public Guid MeetingId { get; init; }
    }
}