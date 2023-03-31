using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBird.Application.DTOs.Invitations;
using BookBird.Application.Helpers;
using BookBird.Domain.Enums;
using BookBird.Infrastructure.EF.Contexts;
using BookBird.Infrastructure.EF.Models;
using BookBird.Infrastructure.Helpers;
using BookBird.Infrastructure.QueryHandlers.Invitations.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.QueryHandlers.Invitations
{
    internal sealed class GetMeetingInvitationsHandler : IRequestHandler<GetMeetingInvitations, IPaginatedList<InvitationDto>>
    {
        private readonly DbSet<InvitationReadModel> _invitations;
        private readonly IMapper _mapper;

        public GetMeetingInvitationsHandler(ReadDbContext context, IMapper mapper)
        {
            _invitations = context.Invitations;
            _mapper = mapper;
        }

        public async Task<IPaginatedList<InvitationDto>> Handle(GetMeetingInvitations request, CancellationToken cancellationToken)
        {
            var invitationQuery = _invitations
                .AsNoTracking()
                .Where(i => i.Status == Status.Active && i.Meeting.Id == request.MeetingId);

            invitationQuery = invitationQuery.OrderByDescending(i => i.CreatedAt);

            var totalCount = await invitationQuery.CountAsync(cancellationToken);

            var invitations = await invitationQuery
                .Skip(request.Skip)
                .Take(request.Take)
                .ProjectTo<InvitationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PaginatedList<InvitationDto>(totalCount, invitations);
        }
    }
}