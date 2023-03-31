using System.Collections.Generic;
using BookBird.Infrastructure.EF.Models.Base;

namespace BookBird.Infrastructure.EF.Models
{
    internal class AuthorReadModel : BaseReadModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<BookReadModel> Books { get; set; }
    }
}