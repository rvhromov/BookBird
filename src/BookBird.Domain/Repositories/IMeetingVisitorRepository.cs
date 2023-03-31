using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookBird.Domain.Entities;

namespace BookBird.Domain.Repositories
{
    public interface IMeetingVisitorRepository
    {
        Task<MeetingVisitor> GetAsync(Guid meetingId, Guid userId);
        Task<List<MeetingVisitor>> GetVisitorsRelatedToUserAsync(Guid userId);
        Task<Guid> AddAsync(MeetingVisitor visitor);
        Task UpdateManyAsync(List<MeetingVisitor> visitors);
        Task UpdateAsync(MeetingVisitor visitor);
    }
}