using System.Linq;
using System.Threading.Tasks;
using BookBird.Application.DTOs.Emails;
using BookBird.Application.IntegrationEvents.Meetings;
using BookBird.Application.Providers;
using BookBird.Domain.Exceptions;
using BookBird.Infrastructure.EF.Contexts;
using BookBird.Infrastructure.EF.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.IntegrationEventHandlers.Meetings
{
    internal sealed class MeetingUpdatedConsumer : IConsumer<MeetingUpdatedIntegrationEvent>
    {
        private readonly DbSet<MeetingReadModel> _meetings;
        private readonly DbSet<UserReadModel> _users;
        private readonly IEmailProvider _emailProvider;

        public MeetingUpdatedConsumer(ReadDbContext context, IEmailProvider emailProvider)
        {
            _meetings = context.Meetings;
            _users = context.Users;
            _emailProvider = emailProvider;
        }

        public async Task Consume(ConsumeContext<MeetingUpdatedIntegrationEvent> context)
        {
            var meeting = await _meetings
                .Include(m => m.Visitors)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == context.Message.MeetingId) 
                ?? throw new NotFoundException("Meeting not found");

            var meetingUserIds = meeting.Visitors
                .Select(v => v.UserId)
                .ToArray();

            if (!meetingUserIds.Any())
            {
                return;
            }

            var users = await _users
                .AsNoTracking()
                .Where(u => meetingUserIds.Contains(u.Id))
                .ToArrayAsync();

            foreach (var user in users)
            {
                var message = CreateEmailMessage(user, meeting);
                await _emailProvider.SendAsync(message);
            }
        }

        private static EmailDto CreateEmailMessage(UserReadModel user, MeetingReadModel meeting) =>
            new()
            {
                ToEmail = user.Email,
                ToName = user.Name,
                Subject = "Meeting Update",
                PlainContent = @$"Hello {user.Name}. The meeting data has been changed. 
                                Meeting {meeting.Name} will take place at {meeting.Location} on {meeting.ScheduledFor:yyyy-MM-dd:hh-mm}"
            };
    }
}