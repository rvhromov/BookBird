using System;
using BookBird.Domain.Enums;

namespace BookBird.Infrastructure.EF.Models.Base
{
    internal abstract class BaseReadModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Status Status { get; set; }
    }
}