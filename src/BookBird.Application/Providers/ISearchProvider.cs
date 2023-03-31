using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookBird.Application.Providers
{
    public interface ISearchProvider
    {
        /// <summary>
        /// Adds or updates collection of the typed JSON documents in a specific index  
        /// </summary>
        Task IndexManyDocumentsAsync<TDoc>(IEnumerable<TDoc> documents) where TDoc : class;

        /// <summary>
        /// Deletes documents in a specific index  
        /// </summary>
        Task DeleteManyDocumentsAsync<TDoc>(IEnumerable<TDoc> documents) where TDoc : class;
    }
}