using System;
using BookBird.Domain.Primitives;

namespace BookBird.Infrastructure.EF.Models
{
    internal class FeedbackReadModel : Entity
    {
        public string Description { get; set; }
        public ushort Rating { get; set; }
        public Guid BookId { get; set; }
        public BookReadModel Book { get; set; }
    }
}