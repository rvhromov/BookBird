using System.Collections.Generic;
using BookBird.Infrastructure.EF.Models.Base;

namespace BookBird.Infrastructure.EF.Models
{
    internal class GenreReadModel : BaseReadModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<BookReadModel> Books { get; set; }
    }
}