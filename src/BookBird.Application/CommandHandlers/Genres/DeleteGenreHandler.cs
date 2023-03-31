using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Genres.Commands;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Repositories;
using MediatR;

namespace BookBird.Application.CommandHandlers.Genres
{
    internal sealed class DeleteGenreHandler : IRequestHandler<DeleteGenre>
    {
        private readonly IGenreRepository _genreRepository;

        public DeleteGenreHandler(IGenreRepository genreRepository) => 
            _genreRepository = genreRepository;
        
        public async Task<Unit> Handle(DeleteGenre request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetAsync(request.Id) 
                ?? throw new NotFoundException("Genre not found.");
            
            genre.Archive();
            await _genreRepository.UpdateAsync(genre);
            
            return Unit.Value;
        }
    }
}