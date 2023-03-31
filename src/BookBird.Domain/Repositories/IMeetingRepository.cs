using System;
using System.Threading.Tasks;
using BookBird.Domain.Entities;

namespace BookBird.Domain.Repositories
{
    public interface IMeetingRepository
    {
        Task<Meeting> GetAsync(Guid id);
        Task<Guid> AddAsync(Meeting meeting);
        Task UpdateAsync(Meeting meeting);
    }
}