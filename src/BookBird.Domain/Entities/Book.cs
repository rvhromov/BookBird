using System;
using System.Collections.Generic;
using System.Linq;
using BookBird.Domain.Enums;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Primitives;
using BookBird.Domain.ValueObjects.Book;

namespace BookBird.Domain.Entities
{
    public class Book : Entity
    {
        internal Book(BookName name, BookPublishYear publishYear, Author author, List<Genre> genres)
        {
            Name = name;
            PublishYear = publishYear;
            Rating = default(double);
            AddAuthor(author);
            AddGenres(genres);
        }

        private Book()
        {
        }
        
        public BookName Name { get; private set; }
        public BookPublishYear PublishYear { get; private set; }
        public BookRating Rating { get; private set; }

        #region Author

        public Guid AuthorId { get; private set; }
        public Author Author { get; private set; }
        
        private void AddAuthor(Author author)
        {
            if (author is null)
                throw new ValidationException("Author cannot be null.");
            
            author.AddBook(this);
            Author = author;
        }

        public void UpdateAuthor(Author author)
        {
            if (author is null)
                throw new ValidationException("Author cannot be null.");
            
            Author.RemoveBook(this);
            author.AddBook(this);
            Author = author;
        }

        #endregion

        #region Genre

        private readonly List<Genre> _genres = new();
        public IReadOnlyCollection<Genre> Genres => _genres.AsReadOnly();
        
        public void SetGenres(List<Genre> genres)
        {
            if (genres is null || !genres.Any())
                throw new ValidationException("Genres cannot be empty.");
            
            foreach (var genre in _genres)
            {
                genre.RemoveBook(this);
            }

            _genres.Clear();
            AddGenres(genres);
        }

        private void AddGenres(List<Genre> genres)
        {
            if (genres is null || !genres.Any())
                throw new ValidationException("Genres cannot be empty.");
            
            foreach (var genre in genres)
            {
                AddGenre(genre);
            }
        }
        
        private void AddGenre(Genre genre)
        {
            if (genre is null)
                throw new ValidationException("Genre cannot be null.");
            
            var alreadyAdded = _genres.Any(g => g.Name == genre.Name);

            if (alreadyAdded)
                throw new ValidationException("Genre already added to the book.");
            
            _genres.Add(genre);
            genre.AddBook(this);
        }
        
        #endregion

        #region Feedback

        private readonly List<Feedback> _feedbacks = new();
        public IReadOnlyCollection<Feedback> Feedbacks => _feedbacks.AsReadOnly();

        internal void AddFeedback(Feedback feedback)
        {
            if (feedback is null)
                throw new ValidationException("Feedback cannot be null.");
            
            _feedbacks.Add(feedback);
        }

        #endregion

        public void Update(BookName name, BookPublishYear publishYear, Author author, List<Genre> genres)
        {
            Name = name;
            PublishYear = publishYear;
            UpdateAuthor(author);
            SetGenres(genres);
            SetModificationDate(DateTime.UtcNow);
        }

        public void CalculateRating()
        {
            var activeFeedbacks = _feedbacks
                .Where(f => f.Status == Status.Active)
                .ToList();

            if (!activeFeedbacks.Any())
            {
                Rating = default;
                return;
            }
            
            var totalRating = activeFeedbacks.Sum(f => f.Rating);
            var rating = (double)totalRating / activeFeedbacks.Count;

            Rating = rating;
            SetModificationDate(DateTime.UtcNow);
        }
        
        public void Archive()
        {
            Status = Status.Deleted;
            SetModificationDate(DateTime.UtcNow);
        }
    }
}