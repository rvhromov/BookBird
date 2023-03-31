using System;
using System.Threading.Tasks;
using BookBird.Domain.Entities;
using BookBird.Domain.Primitives;

namespace BookBird.Domain.Repositories
{
    public interface IAuthorRepository
    {
        Task<Author> GetAsync(Guid id);
        Task<Guid> AddAsync(Author author);
        Task UpdateAsync(Author author);
    }
}