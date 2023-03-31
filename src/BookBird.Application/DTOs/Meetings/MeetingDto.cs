using System;
using BookBird.Domain.Enumerations;

namespace BookBird.Application.DTOs.Meetings
{
    public class MeetingDto : MeetingBaseDto
    {
        public string Location { get; set; }
        public MeetingType Type { get; set; }
        public int? MaxNumberOfVisitors { get; set; }
        public int CurrentNumberOfVisitors { get; set; }
        public Guid OwnerId { get; set; }
    }
}