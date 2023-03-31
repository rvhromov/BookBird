using System.Collections.Generic;
using System.Linq;
using BookBird.Domain.Entities;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Factories.Authors;
using BookBird.Domain.Factories.Books;
using BookBird.Domain.Factories.Genres;
using Shouldly;
using Xunit;

namespace BookBird.UnitTests.Domain
{
    public class BookTests
    {
        private readonly IBookFactory _bookFactory;
        private readonly IAuthorFactory _authorFactory;
        private readonly IGenreFactory _genreFactory;
        
        public BookTests()
        {
            _bookFactory = new BookFactory();
            _authorFactory = new AuthorFactory();
            _genreFactory = new GenreFactory();
        }

        [Fact]
        public void Create_ThrowsValidationException_WhenAuthorIsNull()
        {
            var novelGenre = _genreFactory.Create("Novel", "Novel genre description");
            var satireGenre = _genreFactory.Create("Satire", "Satire genre description");
            var genres = new List<Genre> {novelGenre, satireGenre};
            
            var exception = Record.Exception(() => _bookFactory.Create("The adventures of Tom Sawyer", 1876, null, genres));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ValidationException>();
            exception.Message.ShouldBe("Author cannot be null.");
        }
        
        [Fact]
        public void Create_ThrowsValidationException_WhenGenresAreNull()
        {
            var author = _authorFactory.Create("Mark", "Twain");

            var exception = Record.Exception(() => _bookFactory.Create("The adventures of Tom Sawyer", 1876, author, null));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ValidationException>();
            exception.Message.ShouldBe("Genres cannot be empty.");
        }
        
        [Fact]
        public void Create_ThrowsValidationException_WhenGenresAreEmpty()
        {
            var author = _authorFactory.Create("Mark", "Twain");
            var genres = Enumerable.Empty<Genre>().ToList();

            var exception = Record.Exception(() => _bookFactory.Create("The adventures of Tom Sawyer", 1876, author, genres));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ValidationException>();
            exception.Message.ShouldBe("Genres cannot be empty.");
        }

        [Fact]
        public void Create_ThrowsValidationException_WhenGenresListContainsNullObjects()
        {
            var author = _authorFactory.Create("Mark", "Twain");
            
            var satireGenre = _genreFactory.Create("Satire", "Satire genre description");
            var genres = new List<Genre> {null, satireGenre};
            
            var exception = Record.Exception(() => _bookFactory.Create("The adventures of Tom Sawyer", 1876, author, genres));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ValidationException>();
            exception.Message.ShouldBe("Genre cannot be null.");
        }
        
        [Fact]
        public void UpdateAuthor_ThrowsValidationException_WhenAuthorIsNull()
        {
            var author = _authorFactory.Create("Mark", "Twain");
            
            var novelGenre = _genreFactory.Create("Novel", "Novel genre description");
            var satireGenre = _genreFactory.Create("Satire", "Satire genre description");
            var genres = new List<Genre> {novelGenre, satireGenre};
            
            var book = _bookFactory.Create("The adventures of Tom Sawyer", 1876, author, genres);
            
            var exception = Record.Exception(() => book.UpdateAuthor(null));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ValidationException>();
            exception.Message.ShouldBe("Author cannot be null.");
        }

        [Fact]
        public void UpdateAuthor_AuthorUpdated_WhenSuccess()
        {
            var author = _authorFactory.Create("Mark", "Twain");
            
            var novelGenre = _genreFactory.Create("Novel", "Novel genre description");
            var satireGenre = _genreFactory.Create("Satire", "Satire genre description");
            var genres = new List<Genre> {novelGenre, satireGenre};
            
            var book = _bookFactory.Create("The adventures of Tom Sawyer", 1876, author, genres);

            var newAuthor = _authorFactory.Create("Charles", "Dickens");
            
            book.UpdateAuthor(newAuthor);
            
            book.Author.ShouldNotBeNull();
            book.Author.FirstName.Value.ShouldBe("Charles");
            book.Author.LastName.Value.ShouldBe("Dickens");
            newAuthor.Books.ShouldContain(x => x.Name == "The adventures of Tom Sawyer");
        }

        [Fact]
        public void SetGenres_ThrowsValidationException_WhenGenresAreNull()
        {
            var author = _authorFactory.Create("Mark", "Twain");
            
            var novelGenre = _genreFactory.Create("Novel", "Novel genre description");
            var satireGenre = _genreFactory.Create("Satire", "Satire genre description");
            var genres = new List<Genre> {novelGenre, satireGenre};
            
            var book = _bookFactory.Create("The adventures of Tom Sawyer", 1876, author, genres);

            var exception = Record.Exception(() => book.SetGenres(null));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ValidationException>();
            exception.Message.ShouldBe("Genres cannot be empty.");
        }
        
        [Fact]
        public void SetGenres_ThrowsValidationException_WhenGenresAreEmpty()
        {
            var author = _authorFactory.Create("Mark", "Twain");
            
            var novelGenre = _genreFactory.Create("Novel", "Novel genre description");
            var satireGenre = _genreFactory.Create("Satire", "Satire genre description");
            var genres = new List<Genre> {novelGenre, satireGenre};
            
            var book = _bookFactory.Create("The adventures of Tom Sawyer", 1876, author, genres);

            var exception = Record.Exception(() => book.SetGenres(Enumerable.Empty<Genre>().ToList()));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ValidationException>();
            exception.Message.ShouldBe("Genres cannot be empty.");
        }

        [Fact]
        public void SetGenres_OldGenresSubstitutedWithNewOnes_WhenSuccess()
        {
            var author = _authorFactory.Create("Mark", "Twain");
            
            var novelGenre = _genreFactory.Create("Novel", "Novel genre description");
            var satireGenre = _genreFactory.Create("Satire", "Satire genre description");
            var genres = new List<Genre> {novelGenre, satireGenre};
            
            var book = _bookFactory.Create("The adventures of Tom Sawyer", 1876, author, genres);
            var folkGenre = _genreFactory.Create("Folk", "Folk genre description");

            book.SetGenres(new List<Genre>{folkGenre});
            
            book.Genres.ShouldNotBeNull();
            book.Genres.ShouldNotContain(g => g.Name == novelGenre.Name);
            book.Genres.ShouldNotContain(g => g.Name == satireGenre.Name);
            book.Genres.ShouldContain(g => g.Name == folkGenre.Name);
        }
    }
}