using System.Collections.Generic;
using BookBird.Application.Helpers;

namespace BookBird.Infrastructure.Helpers
{
    internal sealed record PaginatedList<TModel>(int TotalCount, IEnumerable<TModel> Items) : IPaginatedList<TModel> 
        where TModel : class;
}