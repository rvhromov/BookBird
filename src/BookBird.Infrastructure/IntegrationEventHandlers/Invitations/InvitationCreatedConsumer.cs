using System.Threading.Tasks;
using BookBird.Application.DTOs.Emails;
using BookBird.Application.IntegrationEvents.Invitations;
using BookBird.Application.Providers;
using BookBird.Domain.Exceptions;
using BookBird.Infrastructure.EF.Contexts;
using BookBird.Infrastructure.EF.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.IntegrationEventHandlers.Invitations
{
    internal sealed class InvitationCreatedConsumer : IConsumer<InvitationCreatedIntegrationEvent>
    {
        private readonly DbSet<InvitationReadModel> _invitations;
        private readonly IEmailProvider _emailProvider;
        
        public InvitationCreatedConsumer(ReadDbContext context, IEmailProvider emailProvider)
        {
            _invitations = context.Invitations;
            _emailProvider = emailProvider;
        }
        
        public async Task Consume(ConsumeContext<InvitationCreatedIntegrationEvent> context)
        {
            var invitation = await _invitations
                .Include(i => i.Meeting)
                .Include(i => i.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == context.Message.InvitationId) 
                ?? throw new NotFoundException("Invitation not found");
            
            var message = CreateEmailMessage(invitation);

            await _emailProvider.SendAsync(message);
        }
        
        private static EmailDto CreateEmailMessage(InvitationReadModel invitation) =>
            new()
            {
                ToEmail = invitation.User.Email,
                ToName = invitation.User.Name,
                Subject = "Meeting Invitation",
                PlainContent = @$"Hello {invitation.User.Name}. You've been invited to {invitation.Meeting.Name} meeting. 
                                The meeting will take place at {invitation.Meeting.Location} on {invitation.Meeting.ScheduledFor:yyyy-MM-dd:hh-mm}"
            };
    }
}