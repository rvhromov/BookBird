using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBird.Application.DTOs.Meetings;
using BookBird.Domain.Enums;
using BookBird.Domain.Exceptions;
using BookBird.Infrastructure.EF.Contexts;
using BookBird.Infrastructure.EF.Models;
using BookBird.Infrastructure.QueryHandlers.Meetings.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.QueryHandlers.Meetings
{
    internal sealed class GetMeetingHandler : IRequestHandler<GetMeeting, MeetingDto>
    {
        private readonly DbSet<MeetingReadModel> _meetings;
        private readonly IMapper _mapper;

        public GetMeetingHandler(ReadDbContext context, IMapper mapper)
        {
            _meetings = context.Meetings;
            _mapper = mapper;
        }

        public async Task<MeetingDto> Handle(GetMeeting request, CancellationToken cancellationToken) =>
            await _meetings
                .AsNoTracking()
                .Where(m => m.Status == Status.Active)
                .ProjectTo<MeetingDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken) 
                ?? throw new NotFoundException("Meeting not found");
    }
}