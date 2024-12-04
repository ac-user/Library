using Library.Services.Models;
using Library.Models.Media;

namespace Library.Services.Services.Media
{
    public interface ICollectionService : IContentServiceFactory<Collection>
    {
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
