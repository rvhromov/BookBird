using System.Collections.Generic;
using BookBird.Infrastructure.EF.Models.Base;

namespace BookBird.Infrastructure.EF.Models
{
    internal class UserReadModel : BaseReadModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<MeetingReadModel> OwnMeetings { get; set; }
        public ICollection<InvitationReadModel> Invitations { get; set; }
    }
}