using System;
using System.Threading.Tasks;
using BookBird.Application.Services;
using BookBird.Domain.Enums;
using BookBird.Infrastructure.EF.Contexts;
using BookBird.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.Services
{
    internal sealed class InvitationReadService : IInvitationReadService
    {
        private readonly DbSet<InvitationReadModel> _invitations;

        public InvitationReadService(ReadDbContext context) => 
            _invitations = context.Invitations;

        public Task<bool> ExistsAsync(Guid meetingId, Guid userId) =>
            _invitations.AnyAsync(i => i.Status == Status.Active && i.MeetingId == meetingId && i.UserId == userId);
    }
}