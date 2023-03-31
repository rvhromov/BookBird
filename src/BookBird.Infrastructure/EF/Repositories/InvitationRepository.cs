using System;
using System.Threading.Tasks;
using BookBird.Domain.Entities;
using BookBird.Domain.Repositories;
using BookBird.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.EF.Repositories
{
    internal sealed class InvitationRepository : IInvitationRepository
    {
        private readonly WriteDbContext _context;

        public InvitationRepository(WriteDbContext context) => 
            _context = context;

        public Task<Invitation> GetAsync(Guid id) => 
            _context.Invitations
                .Include(i => i.Meeting)
                .SingleOrDefaultAsync(i => i.Id == id);

        public async Task<Guid> AddAsync(Invitation invitation)
        {
            var result = await _context.Invitations.AddAsync(invitation);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task UpdateAsync(Invitation invitation)
        {
            _context.Invitations.Update(invitation);
            await _context.SaveChangesAsync();
        }
    }
}