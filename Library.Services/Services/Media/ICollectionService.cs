using Library.Services.Models;
using Library.Models.Media;

namespace Library.Services.Services.Media
{
    public interface ICollectionService : IContentServiceFactory<Collection>
    {
        /// <summary>
        /// Delete association between content and collection
        /// </summary>
        /// <param name="collectionId">collection item belongs to</param>
        /// <param name="mediaType">type of new media to add</param>
        /// <param name="mediaId">new media to add</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Creation status</returns>
        Task<ResponseStatus> CreateAsync(int collectionId, MediaContentType mediaType, int mediaId, CancellationToken cancellationToken);
        /// <summary>
        /// Delete association between content and collection
        /// </summary>
        /// <param name="collectionId">collection item belongs to</param>
        /// <param name="subId">new subcollection to add</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Creation status</returns>
        Task<ResponseStatus> CreateAsync(int collectionId, int subId, CancellationToken cancellationToken);
        /// <summary>
        /// Delete association between content and collection
        /// </summary>
        /// <param name="collectionId">collection item belongs to</param>
        /// <param name="subId">new subcollection to remove</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Deletion status</returns>
        Task<ResponseStatus> DeleteAsync(int collectionId, int subId, CancellationToken cancellationToken);
        /// <summary>
        /// Delete association between content and collection
        /// </summary>
        /// <param name="id">collection item belongs to</param>
        /// <param name="mediaType">type of content to remove</param>
        /// <param name="mediaId">content to remove</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Deletion status</returns>
        Task<ResponseStatus> DeleteAsync(int id, MediaContentType mediaType, int mediaId, CancellationToken cancellationToken);
    }
}
