using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Invitations.Commands;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Repositories;
using MassTransit;
using MediatR;

namespace BookBird.Application.CommandHandlers.Invitations
{
    internal sealed class DeleteInvitationHandler : IRequestHandler<DeleteInvitation>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public DeleteInvitationHandler(IInvitationRepository invitationRepository, IPublishEndpoint publishEndpoint)
        {
            _invitationRepository = invitationRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(DeleteInvitation request, CancellationToken cancellationToken)
        {
            var invitation = await _invitationRepository.GetAsync(request.Id) 
                ?? throw new NotFoundException("Invitation not found.");

            invitation.Archive();
            await _invitationRepository.UpdateAsync(invitation);

            await _publishEndpoint.Publish(new object(), cancellationToken);
            
            return Unit.Value;
        }
    }
}