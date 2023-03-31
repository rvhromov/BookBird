using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Invitations.Commands;
using BookBird.Application.IntegrationEvents.Invitations;
using BookBird.Domain.Enums;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Repositories;
using MassTransit;
using MediatR;

namespace BookBird.Application.CommandHandlers.Invitations
{
    internal sealed class AcceptInvitationHandler : IRequestHandler<AcceptInvitation>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IMeetingVisitorRepository _meetingVisitorRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public AcceptInvitationHandler(
            IInvitationRepository invitationRepository,  
            IMeetingVisitorRepository meetingVisitorRepository, 
            IPublishEndpoint publishEndpoint)
        {
            _invitationRepository = invitationRepository;
            _meetingVisitorRepository = meetingVisitorRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(AcceptInvitation request, CancellationToken cancellationToken)
        {
            var invitation = await _invitationRepository.GetAsync(request.InvitationId)
                ?? throw new NotFoundException("Invitation not found.");

            if (invitation.InvitationStatus != InvitationStatus.Pending)
                throw new ValidationException("Invitation already been accepted or expired.");

            if (!invitation.IsExpired())
            {
                var meetingVisitor = invitation.Accept();
                await _meetingVisitorRepository.AddAsync(meetingVisitor);
                
                await _publishEndpoint.Publish(new InvitationAcceptedIntegrationEvent(invitation.Id), cancellationToken);
            }
            else
            {
                invitation.Expire();
            }
            
            // TODO: consider Unit of Work pattern
            await _invitationRepository.UpdateAsync(invitation);
            return Unit.Value;
        }
    }
}