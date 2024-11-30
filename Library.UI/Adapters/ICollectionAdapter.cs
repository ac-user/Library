using Library.Models.Media;
using Library.UI.Model;

namespace Library.UI.Adapters
{
    public interface ICollectionAdapter
    {
        /// <summary>
        /// Create a new collection
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="request">collection information</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Creation status</returns>
        Task<CommandResponseStatus> CreateAsync(int accountId, CollectionCreationRequest request, CancellationToken cancellationToken);
        /// <summary>
        /// Update a collection
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="request">information about the collection</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Modification status</returns>
        Task<CommandResponseStatus> ModifyAsync(int accountId, Collection request, CancellationToken cancellationToken);
        /// <summary>
        /// Delete a collection
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="collectionId">collection to delete</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Deletion status</returns>
        Task<CommandResponseStatus> DeleteAsync(int accountId, int collectionId, CancellationToken cancellationToken);
        /// <summary>
        /// Get a specific collection
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="collectionId">collection to get</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Requested collection</returns>
        Task<Collection> GetAsync(int accountId, int collectionId, CancellationToken cancellationToken);
        /// <summary>
        /// Get all the collections for a user
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>All collections for a user</returns>
        Task<List<Collection>> GetAsync(int accountId, CancellationToken cancellationToken);
    }
}
