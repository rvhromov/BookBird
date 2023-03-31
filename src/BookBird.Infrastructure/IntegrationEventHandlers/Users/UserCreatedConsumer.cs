using System.Threading.Tasks;
using BookBird.Application.DTOs.Emails;
using BookBird.Application.IntegrationEvents.Users;
using BookBird.Application.Providers;
using BookBird.Domain.Exceptions;
using BookBird.Infrastructure.EF.Contexts;
using BookBird.Infrastructure.EF.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.IntegrationEventHandlers.Users
{
    internal sealed class UserCreatedConsumer : IConsumer<UserCreatedIntegrationEvent>
    {
        private readonly DbSet<UserReadModel> _users;
        private readonly IEmailProvider _emailProvider;

        public UserCreatedConsumer(ReadDbContext context, IEmailProvider emailProvider)
        {
            _users = context.Users;
            _emailProvider = emailProvider;
        }

        public async Task Consume(ConsumeContext<UserCreatedIntegrationEvent> context)
        {
            var user = await _users.FirstOrDefaultAsync(u => u.Id == context.Message.Id) 
                ?? throw new NotFoundException("User not found");

            var welcomeEmail = new EmailDto
            {
                ToEmail = user.Email,
                ToName = user.Name,
                Subject = "Welcome to BookBird",
                PlainContent = $"Welcome to BookBird {user.Name}. Enjoy your time."
            };

            await _emailProvider.SendAsync(welcomeEmail);
        }
    }
}