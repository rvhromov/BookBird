using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookBird.Domain.Entities;
using BookBird.Domain.Primitives;

namespace BookBird.Domain.Repositories
{
    public interface IGenreRepository
    {
        Task<Genre> GetAsync(Guid id);
        Task<List<Genre>> GetListAsync(IEnumerable<Guid> ids);
        Task<Guid> AddAsync(Genre genre);
        Task UpdateAsync(Genre genre);
    }
}