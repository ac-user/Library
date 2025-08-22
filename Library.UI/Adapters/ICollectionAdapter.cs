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
        /// Create association between content and collection
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="collectionId">the collection to associate to</param>
        /// <param name="mediaType">type of content to associate</param>
        /// <param name="mediaId">content to associate</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Creation status</returns>
        Task<CommandResponseStatus> CreateAsync(int accountId, int collectionId, MediaContentType mediaType, int mediaId, CancellationToken cancellationToken);
        /// <summary>
        /// Create association between two collections
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="collectionId">the collection to associate to</param>
        /// <param name="subId">subcollection to associate</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Creation status</returns>
        Task<CommandResponseStatus> CreateAsync(int accountId, int collectionId, int subId, CancellationToken cancellationToken);
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
        /// Delete a collection's subcollection
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="collectionId">collection to delete</param>
        /// <param name="subId">subcollection to remove</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Deletion status</returns>
        Task<CommandResponseStatus> DeleteAsync(int accountId, int collectionId, int subId, CancellationToken cancellationToken);
        /// <summary>
        /// Delete a collection
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="collectionId">collection to delete</param>
        /// <param name="collectionId">content type of the item</param>
        /// <param name="collectionId">item to delete from collection</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Deletion status</returns>
        Task<CommandResponseStatus> DeleteAsync(int accountId, int collectionId, MediaContentType mediaType, int mediaId, CancellationToken cancellationToken);
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
