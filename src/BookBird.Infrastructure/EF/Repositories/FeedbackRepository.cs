using System;
using System.Threading.Tasks;
using BookBird.Domain.Entities;
using BookBird.Domain.Repositories;
using BookBird.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.EF.Repositories
{
    internal sealed class FeedbackRepository : IFeedbackRepository
    {
        private readonly WriteDbContext _context;

        public FeedbackRepository(WriteDbContext context) => 
            _context = context;
        
        public Task<Feedback> GetAsync(Guid id) => 
            _context.Feedbacks
                .Include(x => x.Book)
                .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<Guid> AddAsync(Feedback feedback)
        {
            var result = await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task UpdateAsync(Feedback feedback)
        {
            _context.Feedbacks.Update(feedback);
            await _context.SaveChangesAsync();
        }
    }
}