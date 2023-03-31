using System;

namespace BookBird.Application.DTOs.Meetings
{
    public class MeetingBaseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ScheduledFor { get; set; }
    }
}