using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Authors;
using BookBird.Application.CommandHandlers.Authors.Commands;
using BookBird.Application.Services;
using BookBird.Domain.Entities;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Factories.Authors;
using BookBird.Domain.Repositories;
using NSubstitute;
using Shouldly;
using Xunit;

namespace BookBird.UnitTests.Application.Authors
{
    public sealed class CreateAuthorHandlerTests
    {
        private readonly CreateAuthorHandler _handler;
        private readonly IAuthorFactory _authorFactory;
        private readonly IAuthorRepository _authorRepository;
        private readonly IAuthorReadService _authorReadService;
        
        public CreateAuthorHandlerTests()
        {
            _authorFactory = Substitute.For<IAuthorFactory>();
            _authorRepository = Substitute.For<IAuthorRepository>();
            _authorReadService = Substitute.For<IAuthorReadService>();

            _handler = new CreateAuthorHandler(_authorFactory, _authorRepository, _authorReadService);
        }

        [Fact]
        public async Task Handle_ThrowsValidationException_WhenAuthorAlreadyExists()
        {
            var request = new CreateAuthor("Mark", "Twain");
            
            _authorReadService.ExistAsync(request.FirstName, request.LastName).Returns(true);

            var exception = await Record.ExceptionAsync(() => _handler.Handle(request, CancellationToken.None));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ValidationException>();
            exception.Message.ShouldBe($"Author {request.FirstName} {request.LastName} already exists.");
        }

        [Fact]
        public async Task Handle_CreatesAuthor_WhenSuccess()
        {
            var request = new CreateAuthor("Mark", "Twain");
            
            _authorReadService.ExistAsync(request.FirstName, request.LastName).Returns(false);
            _authorFactory.Create(request.FirstName, request.LastName).Returns(default(Author));

            var exception = await Record.ExceptionAsync(() => _handler.Handle(request, CancellationToken.None));
            
            exception.ShouldBeNull();
            await _authorRepository.Received(1).AddAsync(Arg.Any<Author>());
        }
    }
}