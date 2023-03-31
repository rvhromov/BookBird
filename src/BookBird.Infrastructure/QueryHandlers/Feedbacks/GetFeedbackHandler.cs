using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBird.Application.DTOs.Feedbacks;
using BookBird.Domain.Enums;
using BookBird.Domain.Exceptions;
using BookBird.Infrastructure.EF.Contexts;
using BookBird.Infrastructure.EF.Models;
using BookBird.Infrastructure.QueryHandlers.Feedbacks.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.QueryHandlers.Feedbacks
{
    internal sealed class GetFeedbackHandler : IRequestHandler<GetFeedback, FeedbackDto>
    {
        private readonly DbSet<FeedbackReadModel> _feedbacks;
        private readonly IMapper _mapper;

        public GetFeedbackHandler(ReadDbContext context, IMapper mapper)
        {
            _feedbacks = context.Feedbacks;
            _mapper = mapper;
        }

        public async Task<FeedbackDto> Handle(GetFeedback request, CancellationToken cancellationToken) =>
            await _feedbacks
                .AsNoTracking()
                .Where(f => f.Status == Status.Active)
                .ProjectTo<FeedbackDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(f => f.Id == request.Id, cancellationToken) 
                ?? throw new NotFoundException("Feedback not found.");
    }
}