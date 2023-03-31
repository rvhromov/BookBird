using System;

namespace BookBird.Application.DTOs.Feedbacks
{
    public class FeedbackDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public ushort Rating { get; set; }
    }
}