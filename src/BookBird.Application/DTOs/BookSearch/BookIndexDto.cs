using System;
using System.Collections.Generic;

namespace BookBird.Application.DTOs.BookSearch
{
    public class BookIndexDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AuthorIndexDto Author { get; set; }
        public List<GenreIndexDto> Genres { get; set; }
    }
}