using Library.Models;
using Library.Models.Adapter.Media.Music;

namespace Library.Adapters
{
    public class MusicAdapter : AdapterBase, IMusicAdapter
    {
        public MusicAdapter(IHttpClientFactory httpClientFactory) : base(httpClientFactory, "") { }

        public async Task<CommandResponseStatus> CreateAsync(int accountId, MusicCreationRequest request, CancellationToken cancellationToken)
        {
            var httpResponse = await MakeCommandRequest<MusicCreationRequest>(HttpMethod.Post,
                                                                            $"api/Account/{accountId}/Library/Media/Music",
                                                                            request,
                                                                            cancellationToken);

            return await GetCommandResponse(httpResponse, cancellationToken);
        }

        public async Task<CommandResponseStatus> ModifyAsync(int accountId, MusicModificationRequest request, CancellationToken cancellationToken)
        {
            var httpResponse = await MakeCommandRequest<MusicModificationRequest>(HttpMethod.Put,
                                                                            $"api/Account/{accountId}/Library/Media/Music",
                                                                            request,
                                                                            cancellationToken);

            return await GetCommandResponse(httpResponse, cancellationToken);
        }

        public async Task<CommandResponseStatus> DeleteAsync(int accountId, int musicId, CancellationToken cancellationToken)
        {
            var httpResponse = await MakeCommandRequest<MusicCreationRequest>(HttpMethod.Delete,
                                                                            $"api/Account/{accountId}/Library/Media/Music/{musicId}",
                                                                            body: null,
                                                                            cancellationToken);

            return await GetCommandResponse(httpResponse, cancellationToken);
        }

        public async Task<Music> GetAsync(int accountId, int musicId, CancellationToken cancellationToken)
        {
            var httpResponse = await MakeQueryRequest($"api/Account/{accountId}/Library/Media/Music/{musicId}",
                                                      cancellationToken);

            return await GetQueryResponse<Music>(httpResponse, cancellationToken);
        }

        public async Task<List<Music>> GetAsync(int accountId, CancellationToken cancellationToken)
        {
            var httpResponse = await MakeQueryRequest($"api/Account/{accountId}/Library/Media/Music",
                                                      cancellationToken);

            return await GetQueryResponse<List<Music>>(httpResponse, cancellationToken);
        }
    }
}
