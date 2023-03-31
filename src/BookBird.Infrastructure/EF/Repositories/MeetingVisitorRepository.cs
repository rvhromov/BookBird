using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBird.Domain.Entities;
using BookBird.Domain.Repositories;
using BookBird.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.EF.Repositories
{
    internal sealed class MeetingVisitorRepository : IMeetingVisitorRepository
    {
        private readonly WriteDbContext _context;

        public MeetingVisitorRepository(WriteDbContext context) => 
            _context = context;

        public Task<MeetingVisitor> GetAsync(Guid meetingId, Guid userId) =>
            _context.MeetingVisitors.SingleOrDefaultAsync(v => v.MeetingId == meetingId && v.UserId == userId);

        public Task<List<MeetingVisitor>> GetVisitorsRelatedToUserAsync(Guid userId) =>
            _context.MeetingVisitors
                .Where(v => v.UserId == userId)
                .ToListAsync();

        public async Task<Guid> AddAsync(MeetingVisitor visitor)
        {
            var result = await _context.MeetingVisitors.AddAsync(visitor);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task UpdateManyAsync(List<MeetingVisitor> visitors)
        {
            foreach (var visitor in visitors)
            {
                _context.MeetingVisitors.Update(visitor);
            }
            
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MeetingVisitor visitor)
        {
            _context.MeetingVisitors.Update(visitor);
            await _context.SaveChangesAsync();
        }
    }
}