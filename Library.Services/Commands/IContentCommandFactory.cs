namespace Library.Services.Commands
{
    public interface IContentCommandFactory<T>
    {
        /// <summary>
        /// Create an entity of type <typeparamref name="T"/>
        /// </summary>
        /// <param name="accountId">account items are created to</param>
        /// <param name="newItem">new item to create</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Creation status</returns>
        Task<int> CreateAsync(int accountId, T newItem, CancellationToken cancellationToken);
        /// <summary>
        /// Update an entity of type <typeparamref name="T"/>
        /// </summary>
        /// <param name="accountId">account items are from</param>
        /// <param name="item">entity with it's modified details</param>
        /// <param name="cancellationToken">token to cancel long running process</param>
        /// <returns>Modification status</returns>
        Task<bool> UpdateAsync(int accountId, T item, CancellationToken cancellationToken);
        /// <summary>
        /// Delete an entity of type <typeparamref name="T"/>
        /// </summary>
        /// <param name="itemId">item to delete</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Deletion status</returns>
        Task<bool> DeleteAsync(int itemId, CancellationToken cancellationToken);
        /// <summary>
        /// Delete all entitities of type <typeparamref name="T"/> associated to an account
        /// </summary>
        /// <param name="accountId">account to delete items from</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Deletion status</returns>
        Task<bool> DeleteAllAsync(int accountId, CancellationToken cancellationToken);
    }
}