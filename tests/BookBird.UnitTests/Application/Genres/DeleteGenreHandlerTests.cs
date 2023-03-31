﻿using System;
using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Genres;
using BookBird.Application.CommandHandlers.Genres.Commands;
using BookBird.Domain.Entities;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Factories.Genres;
using BookBird.Domain.Repositories;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;
using Xunit;

namespace BookBird.UnitTests.Application.Genres
{
    public class DeleteGenreHandlerTests
    {
        private readonly IGenreFactory _genreFactory;
        private readonly IGenreRepository _genreRepository;
        private readonly DeleteGenreHandler _handler;

        public DeleteGenreHandlerTests()
        {
            _genreFactory = new GenreFactory();
            _genreRepository = Substitute.For<IGenreRepository>();

            _handler = new DeleteGenreHandler(_genreRepository);
        }
        
        [Fact]
        public async Task Handle_ThrowsNotFoundException_WhenGenreNotFound()
        {
            var request = new DeleteGenre(Guid.NewGuid());
            
            _genreRepository.GetAsync(request.Id).ReturnsNullForAnyArgs();
            
            var exception = await Record.ExceptionAsync(() => _handler.Handle(request, CancellationToken.None));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<NotFoundException>();
            exception.Message.ShouldBe("Genre not found.");
        }
        
        [Fact]
        public async Task Handle_DeletesGenre_WhenSuccess()
        {
            var request = new DeleteGenre(Guid.NewGuid());
            var genre = _genreFactory.Create("Folk", "Folk genre description");

            _genreRepository.GetAsync(request.Id).Returns(genre);
            
            var exception = await Record.ExceptionAsync(() => _handler.Handle(request, CancellationToken.None));
            
            exception.ShouldBeNull();
            await _genreRepository.Received(1).UpdateAsync(Arg.Any<Genre>());
        }
    }
}