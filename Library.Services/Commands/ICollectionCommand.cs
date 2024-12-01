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
        /// <param name="itemId">item to remvoe</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Deletion status</returns>
        Task<bool> DeleteAsync(int collectionId, int itemId, CancellationToken cancellationToken);
    }
}