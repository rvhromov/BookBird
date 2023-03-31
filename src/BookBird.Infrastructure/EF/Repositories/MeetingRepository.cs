using System;
using System.Threading.Tasks;
using BookBird.Domain.Entities;
using BookBird.Domain.Repositories;
using BookBird.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.EF.Repositories
{
    internal sealed class MeetingRepository : IMeetingRepository
    {
        private readonly WriteDbContext _context;

        public MeetingRepository(WriteDbContext context) => 
            _context = context;

        public Task<Meeting> GetAsync(Guid id) => 
            _context.Meetings.SingleOrDefaultAsync(m => m.Id == id);

        public async Task<Guid> AddAsync(Meeting meeting)
        {
            var result = await _context.Meetings.AddAsync(meeting);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task UpdateAsync(Meeting meeting)
        {
            _context.Meetings.Update(meeting);
            await _context.SaveChangesAsync();
        }
    }
}