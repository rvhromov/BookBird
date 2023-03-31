using System;
using System.Collections.Generic;
using BookBird.Application.DTOs.Genres;

namespace BookBird.Application.DTOs.Books
{
    public class BookExtendedDto : BookDto
    {
        public double Rating { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public IEnumerable<GenreDto> Genres { get; set; }
    }
}