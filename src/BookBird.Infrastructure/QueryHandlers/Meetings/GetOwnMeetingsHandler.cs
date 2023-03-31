using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBird.Application.DTOs.Meetings;
using BookBird.Application.Helpers;
using BookBird.Domain.Enums;
using BookBird.Infrastructure.EF.Contexts;
using BookBird.Infrastructure.EF.Models;
using BookBird.Infrastructure.Helpers;
using BookBird.Infrastructure.QueryHandlers.Meetings.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.QueryHandlers.Meetings
{
    internal sealed class GetOwnMeetingsHandler : IRequestHandler<GetOwnMeetings, IPaginatedList<MeetingBaseDto>>
    {
        private readonly DbSet<MeetingReadModel> _meetings;
        private readonly IMapper _mapper;

        public GetOwnMeetingsHandler(ReadDbContext context, IMapper mapper)
        {
            _meetings = context.Meetings;
            _mapper = mapper;
        }

        public async Task<IPaginatedList<MeetingBaseDto>> Handle(GetOwnMeetings request, CancellationToken cancellationToken)
        {
            var userMeetingsQuery = _meetings
                .AsNoTracking()
                .Where(m => m.Status == Status.Active && m.Owner.Id == request.UserId);

            userMeetingsQuery = userMeetingsQuery.OrderByDescending(m => m.ScheduledFor);

            var totalCount = await userMeetingsQuery.CountAsync(cancellationToken);

            var userMeetings = await userMeetingsQuery
                .Skip(request.Skip)
                .Take(request.Take)
                .ProjectTo<MeetingBaseDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PaginatedList<MeetingBaseDto>(totalCount, userMeetings);
        }
    }
}