using System;

namespace BookBird.Application.DTOs.MeetingVisitors
{
    public class MeetingVisitorDto
    {
        public Guid Id { get; set; }
        public Guid MeetingId { get; set; }
        public string MeetingName { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}