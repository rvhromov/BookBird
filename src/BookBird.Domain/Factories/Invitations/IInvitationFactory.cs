using BookBird.Domain.Entities;

namespace BookBird.Domain.Factories.Invitations
{
    public interface IInvitationFactory
    {
        Invitation Create(Meeting meeting, User user);
    }
}