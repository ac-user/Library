using Library.Models;
using Library.Models.Adapter.Media.Book;

namespace Library.Adapters
{
    public class BookAdapter : AdapterBase, IBookAdapter
    {        
        public BookAdapter(IHttpClientFactory httpClientFactory) : base(httpClientFactory, "LibraryService") { }
        
        public async Task<CommandResponseStatus> CreateAsync(int accountId, BookCreationRequest request, CancellationToken cancellationToken)
        {
            var httpResponse = await MakeCommandRequest<BookCreationRequest>(HttpMethod.Post,
                                                                            $"api/Account/{accountId}/Library/Media/Books",
                                                                            request,
                                                                            cancellationToken);
            
            return await GetCommandResponse(httpResponse, cancellationToken);
        }

        public async Task<CommandResponseStatus> ModifyAsync(int accountId, BookModificationRequest request, CancellationToken cancellationToken)
        {
            var httpResponse = await MakeCommandRequest<BookModificationRequest>(HttpMethod.Put,
                                                                            $"api/Account/{accountId}/Library/Media/Books",
                                                                            request,
                                                                            cancellationToken);

            return await GetCommandResponse(httpResponse, cancellationToken);
        }

        public async Task<CommandResponseStatus> DeleteAsync(int accountId, int bookId, CancellationToken cancellationToken)
        {
            var httpResponse = await MakeCommandRequest<BookCreationRequest>(HttpMethod.Delete,
                                                                            $"api/Account/{accountId}/Library/Media/Books/{bookId}",
                                                                            body: null,
                                                                            cancellationToken);

            return await GetCommandResponse(httpResponse, cancellationToken);
        }

        public async Task<Book> GetAsync(int accountId, int bookId, CancellationToken cancellationToken)
        {
            var httpResponse = await MakeQueryRequest($"api/Account/{accountId}/Library/Media/Books/{bookId}",
                                                      cancellationToken);

            return await GetQueryResponse<Book>(httpResponse, cancellationToken);
        }

        public async Task<List<Book>> GetAsync(int accountId, CancellationToken cancellationToken)
        {
            var httpResponse = await MakeQueryRequest($"api/Account/{accountId}/Library/Media/Books",
                                                      cancellationToken);

            return await GetQueryResponse<List<Book>>(httpResponse, cancellationToken);
        }
    }
}
