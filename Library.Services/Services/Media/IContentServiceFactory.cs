using Library.Services.Models;

namespace Library.Services.Services.Media
{
    public interface IContentServiceFactory<T>
    {
        /// <summary>
        /// Get all the <typeparamref name="T"/> for the user
        /// </summary>
        /// <param name="accountId">account to search</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>List of type <typeparamref name="T"/></returns>
        Task<List<T>> GetAllAsync(int accountId, CancellationToken cancellationToken);
        Task<T> GetAsync(int id, CancellationToken cancellationToken);
        Task<T> GetAsync(int accountId, string creator, CancellationToken cancellationToken);
        Task<ResponseStatus> CreateAsync(T item, CancellationToken cancellationToken);
        Task<ResponseStatus> UpdateAsync(T item, CancellationToken cancellationToken);
        Task<ResponseStatus> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<ResponseStatus> DeleteAllAsync(int accountId, CancellationToken cancellationToken);
    }
}
