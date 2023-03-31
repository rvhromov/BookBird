using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Books.Commands;
using BookBird.Application.Services;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Factories.Books;
using BookBird.Domain.Repositories;
using MediatR;

namespace BookBird.Application.CommandHandlers.Books
{
    internal sealed class CreateBookHandler : IRequestHandler<CreateBook, Guid>
    {
        private readonly IBookFactory _bookFactory;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IBookReadService _bookReadService;

        public CreateBookHandler(
            IBookFactory bookFactory, 
            IBookRepository bookRepository, 
            IAuthorRepository authorRepository, 
            IGenreRepository genreRepository, 
            IBookReadService bookReadService)
        {
            _bookFactory = bookFactory;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
            _bookReadService = bookReadService;
        }

        public async Task<Guid> Handle(CreateBook request, CancellationToken cancellationToken)
        {
            var (name, publishYear, authorId, genreIds) = request;
            
            var author = await _authorRepository.GetAsync(authorId) 
                ?? throw new NotFoundException("Author not found.");

            var genres = await _genreRepository.GetListAsync(genreIds);
            if (genres is null || !genres.Any())
                throw new NotFoundException("Genres not found.");

            var bookAlreadyExists = await _bookReadService.ExistAsync(name, author.FirstName, author.LastName);
            if (bookAlreadyExists)
                throw new ValidationException("Book already exists.");

            var book = _bookFactory.Create(name, publishYear, author, genres);
            return await _bookRepository.AddAsync(book);
        }
    }
}