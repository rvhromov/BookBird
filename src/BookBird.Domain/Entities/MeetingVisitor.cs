using System;
using BookBird.Domain.Enums;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Primitives;

namespace BookBird.Domain.Entities
{
    public class MeetingVisitor : Entity
    {
        internal MeetingVisitor(Invitation invitation)
        {
            UserId = invitation.UserId;
            AddMeeting(invitation.Meeting);
        }
        
        private MeetingVisitor()
        {
        }

        #region User

        public Guid UserId { get; private set; }
        public User User { get; private set; }
        
        private void AddUser(User user)
        {
            if (user is null)
                throw new ValidationException("User cannot be null when adding a visitor.");
            
            User = user;
            UserId = user.Id;
        }
        
        #endregion

        #region Meeting

        public Guid MeetingId { get; private set; }
        public Meeting Meeting { get; private set; }
        
        private void AddMeeting(Meeting meeting)
        {
            if (meeting is null)
                throw new ValidationException("Meeting cannot be null when adding a visitor.");
            
            meeting.AddVisitor(this);
            Meeting = meeting;
            MeetingId = meeting.Id;
        }
        
        #endregion

        public void Archive()
        {
            Status = Status.Deleted;
            SetModificationDate(DateTime.UtcNow);
        }
    }
}