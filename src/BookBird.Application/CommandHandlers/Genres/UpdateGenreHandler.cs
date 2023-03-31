using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Genres.Commands;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Repositories;
using MediatR;

namespace BookBird.Application.CommandHandlers.Genres
{
    internal sealed class UpdateGenreHandler : IRequestHandler<UpdateGenre>
    {
        private readonly IGenreRepository _genreRepository;

        public UpdateGenreHandler(IGenreRepository genreRepository) => 
            _genreRepository = genreRepository;

        public async Task<Unit> Handle(UpdateGenre request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetAsync(request.Id) 
                ?? throw new NotFoundException("Genre not found.");
            
            genre.Update(request.Name, request.Description);
            await _genreRepository.UpdateAsync(genre);
            
            return Unit.Value;
        }
    }
}