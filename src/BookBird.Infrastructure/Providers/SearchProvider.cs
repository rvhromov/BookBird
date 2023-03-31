using System.Collections.Generic;
using System.Threading.Tasks;
using BookBird.Application.Providers;
using BookBird.Domain.Exceptions;
using Nest;

namespace BookBird.Infrastructure.Providers
{
    internal sealed class SearchProvider : ISearchProvider
    {
        private readonly IElasticClient _elasticClient;

        public SearchProvider(IElasticClient elasticClient) => 
            _elasticClient = elasticClient;

        public async Task IndexManyDocumentsAsync<TDoc>(IEnumerable<TDoc> documents) where TDoc : class
        {
            var response = await _elasticClient.IndexManyAsync(documents);
            
            ThrowIfRequestFailed(response);
        }
        
        public async Task DeleteManyDocumentsAsync<TDoc>(IEnumerable<TDoc> documents) where TDoc : class
        {
            var response = await _elasticClient.DeleteManyAsync(documents);
            
            ThrowIfRequestFailed(response);
        }
        
        private static void ThrowIfRequestFailed(IResponse response)
        {
            var exception = response?.OriginalException?.GetBaseException();

            if (exception != null)
            {
                var exceptionMessage = response.OriginalException.Message;
                throw new ValidationException($"Indexing failed with message {exceptionMessage}");
            }
        }
    }
}