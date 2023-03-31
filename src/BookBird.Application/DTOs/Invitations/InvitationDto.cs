using System;
using BookBird.Domain.Enums;

namespace BookBird.Application.DTOs.Invitations
{
    public class InvitationDto
    {
        public Guid Id { get; set; }
        public InvitationStatus InvitationStatus { get; set; }
        public Guid MeetingId { get; set; }
        public string MeetingName { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}