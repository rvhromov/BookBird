using System;
using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Genres.Commands;
using BookBird.Application.Services;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Factories.Genres;
using BookBird.Domain.Repositories;
using MediatR;

namespace BookBird.Application.CommandHandlers.Genres
{
    internal sealed class CreateGenreHandler : IRequestHandler<CreateGenre, Guid>
    {
        private readonly IGenreFactory _genreFactory;
        private readonly IGenreRepository _genreRepository;
        private readonly IGenreReadService _genreReadService;

        public CreateGenreHandler(
            IGenreFactory genreFactory, 
            IGenreRepository genreRepository, 
            IGenreReadService genreReadService)
        {
            _genreFactory = genreFactory;
            _genreRepository = genreRepository;
            _genreReadService = genreReadService;
        }

        public async Task<Guid> Handle(CreateGenre request, CancellationToken cancellationToken)
        {
            var (name, description) = request;
            
            if (await _genreReadService.ExistAsync(name))
                throw new ValidationException($"Genre {name} already exists.");

            var genre = _genreFactory.Create(name, description);
            return await _genreRepository.AddAsync(genre);
        }
    }
}