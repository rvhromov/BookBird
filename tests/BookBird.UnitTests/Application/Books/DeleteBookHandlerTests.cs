using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Books;
using BookBird.Application.CommandHandlers.Books.Commands;
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
    public sealed class DeleteBookHandlerTests
    {
        private readonly IBookFactory _bookFactory;
        private readonly IAuthorFactory _authorFactory;
        private readonly IGenreFactory _genreFactory;
        private readonly IBookRepository _bookRepository;
        
        private readonly DeleteBookHandler _handler;

        public DeleteBookHandlerTests()
        {
            _bookRepository = Substitute.For<IBookRepository>();
            _bookFactory = new BookFactory();
            _authorFactory = new AuthorFactory();
            _genreFactory = new GenreFactory();

            _handler = new DeleteBookHandler(_bookRepository);
        }
        
        [Fact]
        public async Task Handle_ThrowsNotFoundException_WhenBookNotFound()
        {
            var request = new DeleteBook(Guid.NewGuid());

            _bookRepository.GetAsync(request.Id).ReturnsNull();
            
            var exception = await Record.ExceptionAsync(() => _handler.Handle(request, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<NotFoundException>();
            exception.Message.ShouldBe("Book not found.");
        }
        
        [Fact]
        public async Task Handle_UpdateBook_WhenSuccess()
        {
            var request = new DeleteBook(Guid.NewGuid());

            var author = _authorFactory.Create("Mark", "Twain");
            var genre = _genreFactory.Create("Folk", "Folk genre description");
            var genres = new List<Genre> {genre};
            var book = _bookFactory.Create("The adventures of Tom Sawyer", 1877, author, genres);
            
            _bookRepository.GetAsync(request.Id).Returns(book);
            
            var exception = await Record.ExceptionAsync(() => _handler.Handle(request, CancellationToken.None));
            
            exception.ShouldBeNull();
            await _bookRepository.Received(1).UpdateAsync(Arg.Any<Book>());
        }
    }
}