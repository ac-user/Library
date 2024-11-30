using Library.Models.Media;
using Library.UI.Model;

namespace Library.UI.Adapters
{
    public class CollectionAdapter : AdapterBase, ICollectionAdapter
    {
        public CollectionAdapter(IHttpClientFactory httpClientFactory) : base(httpClientFactory, "LibraryService") { }

        public async Task<CommandResponseStatus> CreateAsync(int accountId, CollectionCreationRequest request, CancellationToken cancellationToken)
        {
            var httpResponse = await MakeCommandRequest<CollectionCreationRequest>(HttpMethod.Post,
                                                                            $"api/Account/{accountId}/Library/Media/Collections",
                                                                            request,
                                                                            cancellationToken);

            return await GetCommandResponse(httpResponse, cancellationToken);
        }

        public async Task<CommandResponseStatus> ModifyAsync(int accountId, Collection request, CancellationToken cancellationToken)
        {
            var httpResponse = await MakeCommandRequest<Collection>(HttpMethod.Put,
                                                                            $"api/Account/{accountId}/Library/Media/Collections",
                                                                            request,
                                                                            cancellationToken);

            return await GetCommandResponse(httpResponse, cancellationToken);
        }

        public async Task<CommandResponseStatus> DeleteAsync(int accountId, int collectionId, CancellationToken cancellationToken)
        {
            var httpResponse = await MakeCommandRequest<Collection>(HttpMethod.Delete,
                                                                            $"api/Account/{accountId}/Library/Media/Collections/{collectionId}",
                                                                            body: null,
                                                                            cancellationToken);

            return await GetCommandResponse(httpResponse, cancellationToken);
        }

        public async Task<Collection> GetAsync(int accountId, int collectionId, CancellationToken cancellationToken)
        {
            var httpResponse = await MakeQueryRequest($"api/Account/{accountId}/Library/Media/Collections/{collectionId}",
                                                      cancellationToken);

            return await GetQueryResponse<Collection>(httpResponse, cancellationToken);
        }

        public async Task<List<Collection>> GetAsync(int accountId, CancellationToken cancellationToken)
        {
            var httpResponse = await MakeQueryRequest($"api/Account/{accountId}/Library/Media/Collections",
                                                      cancellationToken);

            return await GetQueryResponse<List<Collection>>(httpResponse, cancellationToken);
        }
    }
}