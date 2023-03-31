using System;
using System.Threading.Tasks;
using BookBird.Domain.Entities;
using BookBird.Domain.Primitives;

namespace BookBird.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<Book> GetAsync(Guid id);
        Task<Guid> AddAsync(Book book);
        Task UpdateAsync(Book book);
    }
}