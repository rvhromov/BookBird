using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.BookSearch.Commands;
using BookBird.Application.Providers;
using BookBird.Application.Services;
using BookBird.Domain.Enums;
using MediatR;

namespace BookBird.Application.CommandHandlers.BookSearch
{
    internal sealed class IndexBooksHandler : IRequestHandler<IndexBooks>
    {
        private readonly IBookReadService _bookReadService;
        private readonly ISearchProvider _searchProvider;

        public IndexBooksHandler(IBookReadService bookReadService, ISearchProvider searchProvider)
        {
            _bookReadService = bookReadService;
            _searchProvider = searchProvider;
        }

        public async Task<Unit> Handle(IndexBooks request, CancellationToken cancellationToken)
        {
            var activeBooks = await _bookReadService.GetBooksForIndexingAsync(Status.Active, request.TakeForLastHours);
            var deletedBooks = await _bookReadService.GetBooksForIndexingAsync(Status.Deleted, request.TakeForLastHours);

            if (activeBooks is not null && activeBooks.Any())
            {
                await _searchProvider.IndexManyDocumentsAsync(activeBooks);
            }
            
            if (deletedBooks is not null && deletedBooks.Any())
            {
                await _searchProvider.DeleteManyDocumentsAsync(deletedBooks);
            }
            
            return Unit.Value;
        }
    }
}