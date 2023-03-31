using System.Threading.Tasks;
using BookBird.Application.Services;
using BookBird.Infrastructure.EF.Contexts;
using BookBird.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.Services
{
    internal sealed class GenreReadService : IGenreReadService
    {
        private readonly DbSet<GenreReadModel> _genres;

        public GenreReadService(ReadDbContext context) =>
            _genres = context.Genres;

        public Task<bool> ExistAsync(string name) =>
            _genres.AnyAsync(g => g.Name == name);
    }
}