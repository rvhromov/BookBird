using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBird.Application.DTOs.Feedbacks;
using BookBird.Application.Helpers;
using BookBird.Domain.Enums;
using BookBird.Infrastructure.EF.Contexts;
using BookBird.Infrastructure.EF.Models;
using BookBird.Infrastructure.Helpers;
using BookBird.Infrastructure.QueryHandlers.Feedbacks.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.QueryHandlers.Feedbacks
{
    internal sealed class GetBookFeedbacksHandler : IRequestHandler<GetBookFeedbacks, IPaginatedList<FeedbackDto>>
    {
        private readonly DbSet<FeedbackReadModel> _feedbacks;
        private readonly IMapper _mapper;

        public GetBookFeedbacksHandler(ReadDbContext context, IMapper mapper)
        {
            _feedbacks = context.Feedbacks;
            _mapper = mapper;
        }
        
        public async Task<IPaginatedList<FeedbackDto>> Handle(GetBookFeedbacks request, CancellationToken cancellationToken)
        {
            var feedbacksQuery = _feedbacks
                .AsNoTracking()
                .Where(f => f.Status == Status.Active && f.Book.Id == request.BookId);

            feedbacksQuery = feedbacksQuery.OrderByDescending(f => f.CreatedAt);

            var totalCount = await feedbacksQuery.CountAsync(cancellationToken);

            var result = await feedbacksQuery
                .Skip(request.Skip)
                .Take(request.Take)
                .ProjectTo<FeedbackDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PaginatedList<FeedbackDto>(totalCount, result);
        }
    }
}