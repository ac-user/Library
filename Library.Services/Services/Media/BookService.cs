using Library.Services.Models;
using Library.Services.Models.Media.Book;

namespace Library.Services.Services.Media
{
    public class BookService : IContentServiceFactory<Book>
    {

        public BookService() { }

        public async Task<List<Book>> GetAllAsync(int accountId, CancellationToken cancellationToken)
        {
            return new List<Book>();
        }

        public async Task<Book> GetAsync(int id, CancellationToken cancellationToken)
        {
            return new Book();
        }

        public async Task<Book> GetAsync(int accountId, string creator, CancellationToken cancellationToken)
        {
            return new Book();
        }

        public async Task<ResponseStatus> CreateAsync(Book item, CancellationToken cancellationToken)
        {
            return new ResponseStatus();
        }


        public async Task<ResponseStatus> UpdateAsync(Book item, CancellationToken cancellationToken)
        {
            return new ResponseStatus();
        }

        public async Task<ResponseStatus> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            return new ResponseStatus();
        }


        public async Task<ResponseStatus> DeleteAllAsync(int accountId, CancellationToken cancellationToken)
        {
            return new ResponseStatus();
        }

    }
}
