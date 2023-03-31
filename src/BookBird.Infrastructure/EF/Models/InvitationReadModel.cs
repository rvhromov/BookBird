using System;
using BookBird.Domain.Enums;
using BookBird.Infrastructure.EF.Models.Base;

namespace BookBird.Infrastructure.EF.Models
{
    internal class InvitationReadModel : BaseReadModel
    {
        public InvitationStatus InvitationStatus { get; set; }
        
        public Guid MeetingId { get; set; }
        public MeetingReadModel Meeting { get; set; }

        public Guid UserId { get; set; }
        public UserReadModel User { get; set; }
    }
}