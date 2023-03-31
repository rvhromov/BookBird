using System;
using System.Threading.Tasks;
using BookBird.Domain.Entities;

namespace BookBird.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<Guid> AddAsync(User user);
        Task UpdateAsync(User user);
    }
}