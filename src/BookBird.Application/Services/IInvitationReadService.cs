using System;
using System.Threading.Tasks;

namespace BookBird.Application.Services
{
    public interface IInvitationReadService
    {
        Task<bool> ExistsAsync(Guid meetingId, Guid userId);
    }
}