using System.Collections.Generic;

namespace BookBird.Application.Helpers
{
    public interface IPaginatedList<TModel> where TModel : class
    {
        public int TotalCount { get; init; }
        public IEnumerable<TModel> Items { get; init; }
    }
}