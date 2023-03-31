using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Books.Commands;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Repositories;
using MediatR;

namespace BookBird.Application.CommandHandlers.Books
{
    internal sealed class UpdateBookHandler : IRequestHandler<UpdateBook>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;

        public UpdateBookHandler(
            IBookRepository bookRepository, 
            IAuthorRepository authorRepository, 
            IGenreRepository genreRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
        }

        public async Task<Unit> Handle(UpdateBook request, CancellationToken cancellationToken)
        {
            var (name, publishYear, authorId, genreIds) = request;
            
            var author = await _authorRepository.GetAsync(authorId) 
                ?? throw new NotFoundException("Author not found.");

            var genres = await _genreRepository.GetListAsync(genreIds);
            if (genres is null || !genres.Any())
                throw new NotFoundException("Genres not found.");

            var book = await _bookRepository.GetAsync(request.Id)
                ?? throw new NotFoundException("Book not found.");
            
            book.Update(name, publishYear, author, genres);
            await _bookRepository.UpdateAsync(book);
            
            return Unit.Value;
        }
    }
}