using System.Collections.Generic;
using BookBird.Application.DTOs.Books;

namespace BookBird.Application.DTOs.Authors
{
    public class AuthorWithBooksDto : AuthorDto
    {
        public IEnumerable<BookDto> Books { get; set; }
    }
}