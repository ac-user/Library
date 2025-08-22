using Library.Models.Media;
using Library.Services.Models;
using Model = Library.Models;

namespace Library.Services.Commands
{
    public interface ICollectionCommand : IContentCommandFactory<Model.Media.Collection>
    {
        /// <summary>
        /// Create association between collection and books
        /// </summary>
        /// <param name="collectionId">collection items belong to</param>
        /// <param name="newItems">new items to associate</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Association id's</returns>
        Task<List<int>> CreateAsync(int collectionId, List<CollectionContentAssociation> newItems, CancellationToken cancellationToken);
        /// <summary>
        /// Create association between collection and sub collections
        /// </summary>
        /// <param name="collectionId">collection items belong to</param>
        /// <param name="newItems">new items to associate</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Association id's</returns>
        Task<List<int>> CreateAsync(int collectionId, List<Model.Media.Collection> newItems, CancellationToken cancellationToken);
        /// <summary>
        /// Delete the association between a collection and media content
        /// </summary>
        /// <param name="collectionId">collection item belongs to</param>
        /// <param name="mediaType">type of item to remove</param>
        /// <param name="itemId">item to remove</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Deletion status</returns>
        Task<bool> DeleteAsync(int collectionId, MediaContentType mediaType, int itemId, CancellationToken cancellationToken);
        /// <summary>
        /// Delete the association between two collections 
        /// </summary>
        /// <param name="collectionId">collection item belongs to</param>
        /// <param name="subId">subcollection to remove</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Deletion status</returns>
        Task<bool> DeleteAsync(int collectionId, int subId, CancellationToken cancellationToken);
    }
}