using BookBird.Domain.Entities;
using BookBird.Domain.Enums;

namespace BookBird.Domain.Factories.Invitations
{
    public class InvitationFactory : IInvitationFactory
    {
        public Invitation Create(Meeting meeting, User user) => 
            new (meeting, user, InvitationStatus.Pending);
    }
}