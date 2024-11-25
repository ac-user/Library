using Library.Models;
using Library.Models.Adapter.Media.Movies;

namespace Library.Adapters
{
    public interface IMovieAdapter
    {
        /// <summary>
        /// Create a new movie
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="request">movie information</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Creation status</returns>
        Task<CommandResponseStatus> CreateAsync(int accountId, MovieCreationRequest request, CancellationToken cancellationToken);
        /// <summary>
        /// Update a movie
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="request">information about the movie</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Modification status</returns>
        Task<CommandResponseStatus> ModifyAsync(int accountId, MovideModificationRequest request, CancellationToken cancellationToken);
        /// <summary>
        /// Delete a movie
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="movieId">movie to delete</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Deletion status</returns>
        Task<CommandResponseStatus> DeleteAsync(int accountId, int movieId, CancellationToken cancellationToken);
        /// <summary>
        /// Get a specific movie
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="movieId">movie to get</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Requested movie</returns>
        Task<Movie> GetAsync(int accountId, int movieId, CancellationToken cancellationToken);
        /// <summary>
        /// Get all the movies for a user
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>All movies for a user</returns>
        Task<List<Movie>> GetAsync(int accountId, CancellationToken cancellationToken);
    }
}
