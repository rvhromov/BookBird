using System.Threading;
using System.Threading.Tasks;
using BookBird.Domain.DomainEvents.Invitations;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Repositories;
using MediatR;

namespace BookBird.Application.DomainEventHandlers.Invitations
{
    internal sealed class InvitationDeletedHandler : INotificationHandler<InvitationDeletedDomainEvent>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IMeetingVisitorRepository _meetingVisitorRepository;

        public InvitationDeletedHandler(
            IInvitationRepository invitationRepository, 
            IMeetingVisitorRepository meetingVisitorRepository)
        {
            _invitationRepository = invitationRepository;
            _meetingVisitorRepository = meetingVisitorRepository;
        }

        public async Task Handle(InvitationDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            var invitation = await _invitationRepository.GetAsync(notification.InvitationId) 
                ?? throw new NotFoundException("Invitation not found.");

            var visitor = await _meetingVisitorRepository.GetAsync(invitation.MeetingId, invitation.UserId);

            if (visitor is null)
            {
                return;
            }
            
            visitor.Archive();
            await _meetingVisitorRepository.UpdateAsync(visitor);
        }
    }
}