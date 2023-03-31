using BookBird.Domain.Entities;
using BookBird.Domain.ValueObjects.Genre;

namespace BookBird.Domain.Factories.Genres
{
    public class GenreFactory : IGenreFactory
    {
        public Genre Create(GenreName name, GenreDescription description) => 
            new(name, description);
    }
}