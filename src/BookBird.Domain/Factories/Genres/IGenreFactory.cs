using BookBird.Domain.Entities;
using BookBird.Domain.ValueObjects.Genre;

namespace BookBird.Domain.Factories.Genres
{
    public interface IGenreFactory
    {
        Genre Create(GenreName name, GenreDescription description);
    }
}