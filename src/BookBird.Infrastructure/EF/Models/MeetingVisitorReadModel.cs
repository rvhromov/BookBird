using System;
using BookBird.Infrastructure.EF.Models.Base;

namespace BookBird.Infrastructure.EF.Models
{
    internal class MeetingVisitorReadModel : BaseReadModel
    {
        public Guid UserId { get; set; }
        public virtual UserReadModel User { get; set; }
        public Guid MeetingId { get; set; }
        public virtual MeetingReadModel Meeting { get; set; }
    }
}