using System.Threading.Tasks;
using BookBird.Application.Services;
using BookBird.Infrastructure.EF.Contexts;
using BookBird.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.Services
{
    internal sealed class AuthorReadService : IAuthorReadService
    {
        private readonly DbSet<AuthorReadModel> _authors;

        public AuthorReadService(ReadDbContext context) => 
            _authors = context.Authors;

        public Task<bool> ExistAsync(string firstName, string lastName) =>
            _authors.AnyAsync(a => a.FirstName == firstName && a.LastName == lastName);
    }
}