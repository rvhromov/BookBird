using System;
using System.Threading.Tasks;
using BookBird.Domain.Entities;
using BookBird.Domain.Repositories;
using BookBird.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.EF.Repositories
{
    internal sealed class BookRepository : IBookRepository
    {
        private readonly WriteDbContext _context;

        public BookRepository(WriteDbContext context) => 
            _context = context;

        public Task<Book> GetAsync(Guid id) => 
            _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genres)
                .Include(b => b.Feedbacks)
                .SingleOrDefaultAsync(b => b.Id == id);

        public async Task<Guid> AddAsync(Book book)
        {
            var result = await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}