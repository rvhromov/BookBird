using System.Threading.Tasks;
using BookBird.Application.Services;
using BookBird.Infrastructure.EF.Contexts;
using BookBird.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.Services
{
    internal sealed class UserReadService : IUserReadService
    {
        private readonly DbSet<UserReadModel> _users;

        public UserReadService(ReadDbContext context) => 
            _users = context.Users;

        public Task<bool> ExistAsync(string email) => 
            _users.AnyAsync(u => u.Email == email);
    }
}