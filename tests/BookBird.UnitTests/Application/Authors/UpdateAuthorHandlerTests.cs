using System;
using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Authors;
using BookBird.Application.CommandHandlers.Authors.Commands;
using BookBird.Domain.Entities;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Factories.Authors;
using BookBird.Domain.Repositories;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;
using Xunit;

namespace BookBird.UnitTests.Application.Authors
{
    public sealed class UpdateAuthorHandlerTests
    {
        private readonly UpdateAuthorHandler _handler;
        private readonly IAuthorRepository _authorRepository;
        private readonly IAuthorFactory _authorFactory;

        public UpdateAuthorHandlerTests()
        {
            _authorFactory = new AuthorFactory();
            _authorRepository = Substitute.For<IAuthorRepository>();
            _handler = new UpdateAuthorHandler(_authorRepository);
        }

        [Fact]
        public async Task Handle_ThrowsNotFoundException_WhenAuthorNotFound()
        {
            var request = new UpdateAuthor("Mark", "Twain")
            {
                Id = Guid.NewGuid()
            };

            _authorRepository.GetAsync(request.Id).ReturnsNullForAnyArgs();
            
            var exception = await Record.ExceptionAsync(() => _handler.Handle(request, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<NotFoundException>();
            exception.Message.ShouldBe("Author not found.");
        }
        
        [Fact]
        public async Task Handle_UpdatesAuthor_WhenSuccess()
        {
            var request = new UpdateAuthor("Mark", "Twain")
            {
                Id = Guid.NewGuid()
            };
            
            var author = _authorFactory.Create("M", "T");

            _authorRepository.GetAsync(request.Id).Returns(author);
            
            var exception = await Record.ExceptionAsync(() => _handler.Handle(request, CancellationToken.None));
            
            exception.ShouldBeNull();
            await _authorRepository.Received(1).UpdateAsync(Arg.Any<Author>());
        }
    }
}