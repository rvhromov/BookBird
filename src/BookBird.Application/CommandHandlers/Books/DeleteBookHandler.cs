using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Books.Commands;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Repositories;
using MediatR;

namespace BookBird.Application.CommandHandlers.Books
{
    internal sealed class DeleteBookHandler : IRequestHandler<DeleteBook>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookHandler(IBookRepository bookRepository) => 
            _bookRepository = bookRepository;

        public async Task<Unit> Handle(DeleteBook request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetAsync(request.Id) 
                ?? throw new NotFoundException("Book not found.");
            
            book.Archive();
            await _bookRepository.UpdateAsync(book);
            
            return Unit.Value;
        }
    }
}