using System;
using System.Collections.Generic;
using System.Linq;
using BookBird.Domain.Enumerations;
using BookBird.Domain.Enums;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Primitives;
using BookBird.Domain.ValueObjects.Meeting;

namespace BookBird.Domain.Entities
{
    public class Meeting : Entity
    {
        internal Meeting(
            MeetingName name, 
            MeetingLocation location, 
            MeetingScheduledFor scheduledFor, 
            MeetingType type,
            MeetingMaxNumberOfVisitors maxNumberOfVisitors,
            MeetingCurrentNumberOfVisitors currentNumberOfVisitors,
            User owner)
        {
            Name = name;
            Location = location;
            ScheduledFor = scheduledFor;
            Type = type;
            MaxNumberOfVisitors = maxNumberOfVisitors;
            CurrentNumberOfVisitors = currentNumberOfVisitors;
            AddOwner(owner);
        }
        
        private Meeting()
        {
        }
        
        public MeetingName Name { get; private set; }
        public MeetingLocation Location { get; private set; }
        public MeetingScheduledFor ScheduledFor { get; private set; }
        public MeetingType Type { get; private set; }
        public MeetingMaxNumberOfVisitors MaxNumberOfVisitors { get; private set; }
        public MeetingCurrentNumberOfVisitors CurrentNumberOfVisitors { get; private set; }
        
        #region Owner

        public Guid OwnerId { get; private set; }
        public User Owner { get; private set; }
        
        private void AddOwner(User owner)
        {
            if (owner is null)
                throw new ValidationException("The meeting owner cannot be null.");

            owner.AddMeeting(this);
            Owner = owner;
        }
        
        #endregion
        
        #region Invitation

        private readonly List<Invitation> _invitations = new();
        public IReadOnlyCollection<Invitation> Invitations => _invitations.AsReadOnly();

        internal void AddInvitation(Invitation invitation)
        {
            if (invitation is null)
                throw new ValidationException("The meeting invitation cannot be null.");

            if (invitation.UserId == OwnerId)
                throw new ValidationException("Unable to invite a meeting owner.");

            var alreadyAdded = _invitations.Any(i => i.UserId == invitation.UserId);
            
            if (alreadyAdded)
                throw new ValidationException("Invitation already added.");

            _invitations.Add(invitation);
        }

        #endregion
        
        #region Visitors

        private readonly List<MeetingVisitor> _visitors = new();
        public IReadOnlyCollection<MeetingVisitor> Visitors => _visitors.AsReadOnly();

        internal void AddVisitor(MeetingVisitor visitor)
        {
            if (visitor is null)
                throw new ValidationException("The meeting visitor cannot be null.");
            
            if (visitor.UserId == OwnerId)
                throw new ValidationException("The meeting visitor cannot be the meeting owner.");

            var alreadyAdded = _visitors.Any(v => v.UserId == visitor.UserId);
            
            if (alreadyAdded)
                throw new ValidationException("Visitor already added to the meeting.");

            _visitors.Add(visitor);
            CurrentNumberOfVisitors++;
        }

        internal void RemoveVisitor(MeetingVisitor visitor)
        {
            if (visitor is null)
                throw new ValidationException("Unable to remove visitor from the meeting because visitor is null.");

            var currentVisitor = _visitors.SingleOrDefault(i => i.Id == visitor.Id);
            
            if (currentVisitor is null)
                return;
            
            _visitors.Remove(currentVisitor);
            CurrentNumberOfVisitors--;
        }
        
        #endregion

        public void Update(
            MeetingName name, 
            MeetingLocation location, 
            MeetingScheduledFor scheduledFor,
            MeetingMaxNumberOfVisitors numberOfVisitors)
        {
            Name = name;
            Location = location;
            ScheduledFor = scheduledFor;
            MaxNumberOfVisitors = numberOfVisitors;
            SetModificationDate(DateTime.UtcNow);
        }

        public void Archive()
        {
            Status = Status.Deleted;
            SetModificationDate(DateTime.UtcNow);
        }
    }
}