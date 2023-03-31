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
    internal sealed class GenreRepository : IGenreRepository
    {
        private readonly WriteDbContext _context;

        public GenreRepository(WriteDbContext context) => 
            _context = context;

        public Task<Genre> GetAsync(Guid id) => 
            _context.Genres.SingleOrDefaultAsync(g => g.Id == id);

        public Task<List<Genre>> GetListAsync(IEnumerable<Guid> ids) => 
            _context.Genres
                .Where(g => ids.Contains(g.Id))
                .ToListAsync();

        public async Task<Guid> AddAsync(Genre genre)
        {
            var result = await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task UpdateAsync(Genre genre)
        {
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
        }
    }
}