using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Genres;
using BookBird.Application.CommandHandlers.Genres.Commands;
using BookBird.Application.Services;
using BookBird.Domain.Entities;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Factories.Genres;
using BookBird.Domain.Repositories;
using NSubstitute;
using Shouldly;
using Xunit;

namespace BookBird.UnitTests.Application.Genres
{
    public sealed class CreateGenreHandlerTests
    {
        private readonly CreateGenreHandler _handler;
        private readonly IGenreFactory _genreFactory;
        private readonly IGenreRepository _genreRepository;
        private readonly IGenreReadService _genreReadService;
        
        public CreateGenreHandlerTests()
        {
            _genreFactory = Substitute.For<IGenreFactory>();
            _genreRepository = Substitute.For<IGenreRepository>();
            _genreReadService = Substitute.For<IGenreReadService>();
            
            _handler = new CreateGenreHandler(_genreFactory, _genreRepository, _genreReadService);
        }

        [Fact]
        public async Task Handle_ThrowsNotFoundException_WhenGenreNotFound()
        {
            var request = new CreateGenre("Folk", "Folk genre description");

            _genreReadService.ExistAsync(request.Name).Returns(true);
            
            var exception = await Record.ExceptionAsync(() => _handler.Handle(request, CancellationToken.None));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ValidationException>();
            exception.Message.ShouldBe($"Genre {request.Name} already exists.");
        }
        
        [Fact]
        public async Task Handle_CreatesGenre_WhenSuccess()
        {
            var request = new CreateGenre("Folk", "Folk genre description");
            
            _genreReadService.ExistAsync(request.Name).Returns(false);
            _genreFactory.Create(request.Name, request.Description).Returns(default(Genre));
            
            var exception = await Record.ExceptionAsync(() => _handler.Handle(request, CancellationToken.None));
            
            exception.ShouldBeNull();
            await _genreRepository.Received(1).AddAsync(Arg.Any<Genre>());
        }
    }
}