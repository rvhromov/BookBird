using System;

namespace BookBird.Application.DTOs.BookSearch
{
    public class AuthorIndexDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}