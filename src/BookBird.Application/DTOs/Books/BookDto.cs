using System;

namespace BookBird.Application.DTOs.Books
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ushort PublishYear { get; set; }
    }
}