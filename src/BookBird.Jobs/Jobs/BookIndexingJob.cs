using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.BookSearch.Commands;
using MediatR;
using Quartz;

namespace BookBird.Jobs.Jobs
{
    internal sealed class BookIndexingJob : IJob
    {
        public static readonly JobKey Key = new(nameof(BookIndexingJob));
        
        private readonly IMediator _mediator;

        public BookIndexingJob(IMediator mediator) => 
            _mediator = mediator;

        public async Task Execute(IJobExecutionContext context) => 
            await _mediator.Send(new IndexBooks());
    }
}