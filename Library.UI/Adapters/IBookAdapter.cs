using Library.Models;
using Library.UI.Model;
using Library.Models.Media.Book;

namespace Library.UI.Adapters
{
    public interface IBookAdapter
    {
        /// <summary>
        /// Create a new book
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="request">book information</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Creation status</returns>
        Task<CommandResponseStatus> CreateAsync(int accountId, BookCreationRequest request, CancellationToken cancellationToken);
        /// <summary>
        /// Update a book
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="request">information about the book</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Modification status</returns>
        Task<CommandResponseStatus> ModifyAsync(int accountId, BookModificationRequest request, CancellationToken cancellationToken);
        /// <summary>
        /// Delete a book
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="bookId">book to delete</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Deletion status</returns>
        Task<CommandResponseStatus> DeleteAsync(int accountId, int bookId, CancellationToken cancellationToken);
        /// <summary>
        /// Get a specific book
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="bookId">book to get</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>Requested book</returns>
        Task<Book> GetAsync(int accountId, int bookId, CancellationToken cancellationToken);
        /// <summary>
        /// Get all the books for a user
        /// </summary>
        /// <param name="accountId">the users account</param>
        /// <param name="cancellationToken">token to cancel long running processes</param>
        /// <returns>All books for a user</returns>
        Task<List<Book>> GetAsync(int accountId, CancellationToken cancellationToken);
    }
}
