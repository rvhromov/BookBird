using System;
using System.Collections.Generic;
using BookBird.Infrastructure.EF.Models.Base;

namespace BookBird.Infrastructure.EF.Models
{
    internal class BookReadModel : BaseReadModel
    {
        public string Name { get; set; }
        public ushort PublishYear { get; set; }
        public double Rating { get; set; }
        public Guid AuthorId { get; set; }
        public virtual AuthorReadModel Author { get; set; }
        public ICollection<GenreReadModel> Genres { get; set; }
        public ICollection<FeedbackReadModel> Feedbacks { get; set; }
    }
}