using System;
using System.Threading.Tasks;
using BookBird.Domain.Entities;
using BookBird.Domain.Primitives;

namespace BookBird.Domain.Repositories
{
    public interface IFeedbackRepository
    {
        Task<Feedback> GetAsync(Guid id);
        Task<Guid> AddAsync(Feedback feedback);
        Task UpdateAsync(Feedback feedback);
    }
}