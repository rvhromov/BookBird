using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBird.Application.DTOs.MeetingVisitors;
using BookBird.Application.Helpers;
using BookBird.Domain.Enums;
using BookBird.Infrastructure.EF.Contexts;
using BookBird.Infrastructure.EF.Models;
using BookBird.Infrastructure.Helpers;
using BookBird.Infrastructure.QueryHandlers.MeetingVisitors.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.QueryHandlers.MeetingVisitors
{
    internal sealed class GetMeetingVisitorsHandler : IRequestHandler<GetMeetingVisitors, IPaginatedList<MeetingVisitorDto>>
    {
        private readonly DbSet<MeetingVisitorReadModel> _visitors;
        private readonly IMapper _mapper;

        public GetMeetingVisitorsHandler(ReadDbContext context, IMapper mapper)
        {
            _visitors = context.MeetingVisitors;
            _mapper = mapper;
        }

        public async Task<IPaginatedList<MeetingVisitorDto>> Handle(GetMeetingVisitors request, CancellationToken cancellationToken)
        {
            var visitorsQuery = _visitors
                .AsNoTracking()
                .Where(v => v.Status == Status.Active && v.MeetingId == request.MeetingId)
                .ProjectTo<MeetingVisitorDto>(_mapper.ConfigurationProvider);

            visitorsQuery = visitorsQuery.OrderBy(v => v.UserName);

            var totalCount = await visitorsQuery.CountAsync(cancellationToken);

            var visitors = await visitorsQuery
                .Skip(request.Skip)
                .Take(request.Take)
                .ToListAsync(cancellationToken);

            return new PaginatedList<MeetingVisitorDto>(totalCount, visitors);
        }
    }
}