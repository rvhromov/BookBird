using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Books;
using BookBird.Application.CommandHandlers.Books.Commands;
using BookBird.Application.Services;
using BookBird.Domain.Entities;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Factories.Authors;
using BookBird.Domain.Factories.Books;
using BookBird.Domain.Factories.Genres;
using BookBird.Domain.Repositories;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;
using Xunit;

namespace BookBird.UnitTests.Application.Books
{
    public sealed class UpdateBookHandlerTests
    {
        private readonly IBookFactory _bookFactory;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IAuthorFactory _authorFactory;
        private readonly IGenreFactory _genreFactory;
        private readonly UpdateBookHandler _handler;
        
        public UpdateBookHandlerTests()
        {
            _bookRepository = Substitute.For<IBookRepository>();
            _authorRepository = Substitute.For<IAuthorRepository>();
            _genreRepository = Substitute.For<IGenreRepository>();
            _authorFactory = new AuthorFactory();
            _genreFactory = new GenreFactory();
            _bookFactory = new BookFactory();
            
            _handler = new UpdateBookHandler(_bookRepository, _authorRepository, _genreRepository);
        }
        
        [Fact]
        public async Task Handle_ThrowsNotFoundException_WhenAuthorNotFound()
        {
            var request = new UpdateBook("The adventures of Tom Sawyer", 1876, Guid.NewGuid(), new List<Guid>{Guid.NewGuid()})
            {
                Id = Guid.NewGuid()
            };
            
            _authorRepository.GetAsync(request.AuthorId).ReturnsNullForAnyArgs();
            
            var exception = await Record.ExceptionAsync(() => _handler.Handle(request, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<NotFoundException>();
            exception.Message.ShouldBe("Author not found.");
        }
        
        [Fact]
        public async Task Handle_ThrowsNotFoundException_WhenGenreNotFound()
        {
            var request = new UpdateBook("The adventures of Tom Sawyer", 1876, Guid.NewGuid(), new List<Guid>{Guid.NewGuid()})
            {
                Id = Guid.NewGuid()
            };

            var author = _authorFactory.Create("Mark", "Twain");
            
            _authorRepository.GetAsync(request.AuthorId).Returns(author);
            _genreRepository.GetListAsync(request.GenreIds).Returns(Enumerable.Empty<Genre>().ToList());
            
            var exception = await Record.ExceptionAsync(() => _handler.Handle(request, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<NotFoundException>();
            exception.Message.ShouldBe("Genres not found.");
        }
        
        [Fact]
        public async Task Handle_ThrowsNotFoundException_WhenBookNotFound()
        {
            var request = new UpdateBook("The adventures of Tom Sawyer", 1876, Guid.NewGuid(), new List<Guid>{Guid.NewGuid()})
            {
                Id = Guid.NewGuid()
            };

            var author = _authorFactory.Create("Mark", "Twain");
            var genre = _genreFactory.Create("Folk", "Folk genre description");
            var genres = new List<Genre> {genre};
            
            _authorRepository.GetAsync(request.AuthorId).Returns(author);
            _genreRepository.GetListAsync(request.GenreIds).Returns(genres);
            _bookRepository.GetAsync(request.Id).ReturnsNull();
            
            var exception = await Record.ExceptionAsync(() => _handler.Handle(request, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<NotFoundException>();
            exception.Message.ShouldBe("Book not found.");
        }
        
        [Fact]
        public async Task Handle_UpdateBook_WhenSuccess()
        {
            var request = new UpdateBook("The adventures of Tom Sawyer", 1876, Guid.NewGuid(), new List<Guid>{Guid.NewGuid()})
            {
                Id = Guid.NewGuid()
            };

            var author = _authorFactory.Create("Mark", "Twain");
            var genre = _genreFactory.Create("Folk", "Folk genre description");
            var genres = new List<Genre> {genre};
            var book = _bookFactory.Create("The adventures of Tom Sawyer", 1877, author, genres);
            
            _authorRepository.GetAsync(request.AuthorId).Returns(author);
            _genreRepository.GetListAsync(request.GenreIds).Returns(genres);
            _bookRepository.GetAsync(request.Id).Returns(book);
            
            var exception = await Record.ExceptionAsync(() => _handler.Handle(request, CancellationToken.None));
            
            exception.ShouldBeNull();
            await _bookRepository.Received(1).UpdateAsync(Arg.Any<Book>());
        }
    }
}