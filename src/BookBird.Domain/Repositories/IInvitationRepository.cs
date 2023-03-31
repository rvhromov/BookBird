using System;
using System.Threading.Tasks;
using BookBird.Domain.Entities;

namespace BookBird.Domain.Repositories
{
    public interface IInvitationRepository
    {
        Task<Invitation> GetAsync(Guid id);
        Task<Guid> AddAsync(Invitation invitation);
        Task UpdateAsync(Invitation invitation);
    }
}