using System;
using System.Threading.Tasks;
using BookBird.Domain.Entities;
using BookBird.Domain.Repositories;
using BookBird.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.EF.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly WriteDbContext _context;

        public UserRepository(WriteDbContext context) => 
            _context = context;
        
        public Task<User> GetAsync(Guid id) => 
            _context.Users.SingleOrDefaultAsync(u => u.Id == id);
        
        public async Task<Guid> AddAsync(User user)
        {
            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}