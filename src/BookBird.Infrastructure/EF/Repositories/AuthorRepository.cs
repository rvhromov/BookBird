using System;
using System.Threading.Tasks;
using BookBird.Domain.Entities;
using BookBird.Domain.Repositories;
using BookBird.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.EF.Repositories
{
    internal sealed class AuthorRepository : IAuthorRepository
    {
        private readonly WriteDbContext _context;

        public AuthorRepository(WriteDbContext context) => 
            _context = context;

        public Task<Author> GetAsync(Guid id) => 
            _context.Authors
                .Include(a => a.Books)
                .SingleOrDefaultAsync(a => a.Id == id);

        public async Task<Guid> AddAsync(Author author)
        {
            var result = await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task UpdateAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }
    }
}