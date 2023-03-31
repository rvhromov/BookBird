using System;
using System.Collections.Generic;
using BookBird.Domain.Enumerations;
using BookBird.Infrastructure.EF.Models.Base;

namespace BookBird.Infrastructure.EF.Models
{
    internal class MeetingReadModel : BaseReadModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime ScheduledFor { get; set; }
        public MeetingType Type { get; set; }
        public int? MaxNumberOfVisitors { get; set; }
        public int CurrentNumberOfVisitors { get; set; }

        public Guid OwnerId { get; set; }
        public UserReadModel Owner { get; set; }

        public ICollection<InvitationReadModel> Invitations { get; set; }
        public ICollection<MeetingVisitorReadModel> Visitors { get; set; }
    }
}