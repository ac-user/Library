namespace Library.Services.Queries
{
    public interface IContentQueryFactory<T>
    {
        /// <summary>
        /// Get <typeparamref name="T"/>
        /// </summary>
        /// <param name="itemId"><typeparamref name="T"/> identifier</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns><typeparamref name="T"/></returns>
        Task<T> GetAsync(int itemId, CancellationToken cancellationToken);
        /// <summary>
        /// Get all <typeparamref name="T"/> on account
        /// </summary>
        /// <param name="accountId">account to search</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>List of <typeparamref name="T"/></returns>
        Task<List<T>> GetAllAsync(int accountId, CancellationToken cancellationToken);
    }
}
