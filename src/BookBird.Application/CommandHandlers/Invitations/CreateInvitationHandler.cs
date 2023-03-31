using System;
using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Invitations.Commands;
using BookBird.Application.IntegrationEvents.Invitations;
using BookBird.Application.Services;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Factories.Invitations;
using BookBird.Domain.Repositories;
using MassTransit;
using MediatR;

namespace BookBird.Application.CommandHandlers.Invitations
{
    internal sealed class CreateInvitationHandler : IRequestHandler<CreateInvitation, Guid>
    {
        private readonly IInvitationFactory _invitationFactory;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IInvitationRepository _invitationRepository;
        private readonly IInvitationReadService _invitationReadService;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateInvitationHandler(
            IInvitationFactory invitationFactory,
            IMeetingRepository meetingRepository,
            IUserRepository userRepository, 
            IInvitationRepository invitationRepository, 
            IInvitationReadService invitationReadService, 
            IPublishEndpoint publishEndpoint)
        {
            _invitationFactory = invitationFactory;
            _meetingRepository = meetingRepository;
            _userRepository = userRepository;
            _invitationRepository = invitationRepository;
            _invitationReadService = invitationReadService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Guid> Handle(CreateInvitation request, CancellationToken cancellationToken)
        {
            var (meetingId, userId) = request;

            if (await _invitationReadService.ExistsAsync(meetingId, userId))
                throw new ValidationException("Invitation already created.");

            var meeting = await _meetingRepository.GetAsync(meetingId)
                ?? throw new NotFoundException("Meeting not found.");

            var user = await _userRepository.GetAsync(userId)
                ?? throw new NotFoundException("User not found.");
            
            var invitation = _invitationFactory.Create(meeting, user);
            var id = await _invitationRepository.AddAsync(invitation);

            await _publishEndpoint.Publish(new InvitationCreatedIntegrationEvent(invitation.Id), cancellationToken);
            
            return id;
        }
    }
}