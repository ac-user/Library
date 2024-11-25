using Library.Models;
using Library.Models.Adapter.Media.Music;

namespace Library.Adapters
{
    public interface IMusicAdapter
    {
        /// <summary>
        /// Create a new music
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="request">music information</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Creation status</returns>
        Task<CommandResponseStatus> CreateAsync(int accountId, MusicCreationRequest request, CancellationToken cancellationToken);
        /// <summary>
        /// Update a music
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="request">information about the music</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Modification status</returns>
        Task<CommandResponseStatus> ModifyAsync(int accountId, MusicModificationRequest request, CancellationToken cancellationToken);
        /// <summary>
        /// Delete a music
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="musicId">music to delete</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Deletion status</returns>
        Task<CommandResponseStatus> DeleteAsync(int accountId, int musicId, CancellationToken cancellationToken);
        /// <summary>
        /// Get a specific music
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="musicId">music to get</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Requested music</returns>
        Task<Music> GetAsync(int accountId, int musicId, CancellationToken cancellationToken);
        /// <summary>
        /// Get all the musics for a user
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>All musics for a user</returns>
        Task<List<Music>> GetAsync(int accountId, CancellationToken cancellationToken);
    }
}
