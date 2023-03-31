using System;
using BookBird.Domain.DomainEvents.Invitations;
using BookBird.Domain.Enumerations;
using BookBird.Domain.Enums;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Primitives;

namespace BookBird.Domain.Entities
{
    public class Invitation : Entity
    {
        internal Invitation(Meeting meeting, User user, InvitationStatus invitationStatus)
        {
            InvitationStatus = invitationStatus;
            AddMeeting(meeting);
            AddUser(user);
        }

        private Invitation()
        {
        }
        
        public InvitationStatus InvitationStatus { get; private set; }

        #region Meeting

        public Guid MeetingId { get; private set; }
        public Meeting Meeting { get; private set; }

        private void AddMeeting(Meeting meeting)
        {
            if (meeting is null)
                throw new ValidationException("Meeting cannot be null when creating invitation.");

            meeting.AddInvitation(this);
            Meeting = meeting;
        }
        
        #endregion

        #region User

        public Guid UserId { get; private set; }
        public User User { get; private set; }

        private void AddUser(User user)
        {
            if (user is null)
                throw new ValidationException("User cannot be null when creating invitation.");

            user.AddInvitation(this);
            User = user;
        }
        
        #endregion

        public void Archive()
        {
            Status = Status.Deleted;
            SetModificationDate(DateTime.UtcNow);

            AddEvent(new InvitationDeletedDomainEvent(Id));
        }

        public void Expire()
        {
            InvitationStatus = InvitationStatus.Expired;
            SetModificationDate(DateTime.UtcNow);
        }

        public MeetingVisitor Accept()
        {
            InvitationStatus = InvitationStatus.Accepted;
            SetModificationDate(DateTime.UtcNow);

            return new MeetingVisitor(this);
        }
        
        public bool IsExpired() => 
            Meeting.ScheduledFor < DateTime.UtcNow || 
            Meeting.Type.Equals(MeetingType.Limited) && 
            Meeting.CurrentNumberOfVisitors == Meeting.MaxNumberOfVisitors;
    }
}