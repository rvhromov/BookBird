using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.DTOs.BookSearch;
using BookBird.Infrastructure.QueryHandlers.BookSearch.Queries;
using MediatR;
using Nest;

namespace BookBird.Infrastructure.QueryHandlers.BookSearch
{
    internal sealed class SearchBookHandler : IRequestHandler<SearchBook, IReadOnlyCollection<BookIndexDto>>
    {
        private readonly IElasticClient _elasticClient;

        public SearchBookHandler(IElasticClient elasticClient) => 
            _elasticClient = elasticClient;

        public async Task<IReadOnlyCollection<BookIndexDto>> Handle(SearchBook request, CancellationToken cancellationToken)
        {
            var (skip, take, searchTerm) = request;
            
            Func<SearchDescriptor<BookIndexDto>, ISearchRequest> searchQuery = s => s
                .From(skip)
                .Size(take)
                .Query(q => q
                    .QueryString(qs => qs
                        .AnalyzeWildcard()
                        .Query($"*{searchTerm.ToLower()}*")
                        .Fields(fs => fs
                            .Fields(f1 => f1.Name, f2 => f2.Author.FirstName, f3 => f3.Author.LastName, f4 => f4.Genres.First().Name))));
            
            var books = await _elasticClient.SearchAsync(searchQuery, cancellationToken);

            return books.Documents;
        }
    }
}