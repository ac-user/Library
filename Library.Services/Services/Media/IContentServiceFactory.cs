using Library.Services.Models;

namespace Library.Services.Services.Media
{
    public interface IContentServiceFactory<T>
    {
        /// <summary>
        /// Get <typeparamref name="T"/> 
        /// </summary>
        /// <param name="id"><typeparamref name="T"/> to get</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns><typeparamref name="T"/></returns>
        Task<T> GetAsync(int id, CancellationToken cancellationToken);
        /// <summary>
        /// Get all the <typeparamref name="T"/> for the user
        /// </summary>
        /// <param name="accountId">account to search</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>List of type <typeparamref name="T"/></returns>
        Task<List<T>> GetAllAsync(int accountId, CancellationToken cancellationToken);
        /// <summary>
        /// Create a new <typeparamref name="T"/>
        /// </summary>
        /// <param name="accountId">account to search</param>
        /// <param name="item">new <typeparamref name="T"/> to create</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Status of creation</returns>
        Task<ResponseStatus> CreateAsync(int accountId, T item, CancellationToken cancellationToken);
        /// <summary>
        /// Modify <typeparamref name="T"/>
        /// </summary>
        /// <param name="accountId">account to search</param>
        /// <param name="item"><typeparamref name="T"/> new information</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Status of modification</returns>
        Task<ResponseStatus> UpdateAsync(int accountId, T item, CancellationToken cancellationToken);
        /// <summary>
        /// Delete <typeparamref name="T"/>
        /// </summary>
        /// <param name="id"><typeparamref name="T"/> to delete</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Status of deletion</returns>
        Task<ResponseStatus> DeleteAsync(int id, CancellationToken cancellationToken);
        /// <summary>
        /// Delete all <typeparamref name="T"/> for the account
        /// </summary>
        /// <param name="accountId">account to delete <typeparamref name="T"/> from</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Status of deletion</returns>
        Task<ResponseStatus> DeleteAllAsync(int accountId, CancellationToken cancellationToken);
    }
}
