using System;
using System.Collections.Generic;
using BookBird.Domain.DomainEvents.Users;
using BookBird.Domain.Enums;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Primitives;
using BookBird.Domain.ValueObjects.User;

namespace BookBird.Domain.Entities
{
    public class User : Entity
    {
        internal User(UserName name, UserEmail email)
        {
            Name = name;
            Email = email;
        }

        private User()
        {
        }
        
        public UserName Name { get; private set; }
        public UserEmail Email { get; private set; }

        #region OwnMeetings

        private readonly List<Meeting> _ownMeetings = new();
        public IReadOnlyCollection<Meeting> OwnMeetings => _ownMeetings.AsReadOnly();
        
        internal void AddMeeting(Meeting meeting)
        {
            if (meeting is null)
                throw new ValidationException("Meeting cannot be null when adding to user.");
            
            _ownMeetings.Add(meeting);
        }
        
        #endregion

        #region Invitations

        private readonly List<Invitation> _invitations = new();
        public IReadOnlyCollection<Invitation> Invitations => _invitations.AsReadOnly();

        internal void AddInvitation(Invitation invitation)
        {
            if (invitation is null)
                throw new ValidationException("Invitation cannot be null when adding to user.");
            
            _invitations.Add(invitation);
        }
        
        #endregion

        public void Update(UserName name, UserEmail email)
        {
            Name = name;
            Email = email;
            SetModificationDate(DateTime.UtcNow);
        }

        public void Archive()
        {
            Status = Status.Deleted;
            SetModificationDate(DateTime.UtcNow);
            
            AddEvent(new UserDeletedDomainEvent(Id));
        }
    }
}