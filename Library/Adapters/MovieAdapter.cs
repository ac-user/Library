using Library.Models;
using Library.Models.Adapter.Media.Movies;

namespace Library.Adapters
{
    public class MovieAdapter : AdapterBase, IMovieAdapter
    {
        public MovieAdapter(IHttpClientFactory httpClientFactory) : base(httpClientFactory, "LibraryService") { }

        public async Task<CommandResponseStatus> CreateAsync(int accountId, MovieCreationRequest request, CancellationToken cancellationToken)
        {
            var httpResponse = await MakeCommandRequest<MovieCreationRequest>(HttpMethod.Post,
                                                                            $"api/Account/{accountId}/Library/Media/Movies",
                                                                            request,
                                                                            cancellationToken);

            return await GetCommandResponse(httpResponse, cancellationToken);
        }

        public async Task<CommandResponseStatus> ModifyAsync(int accountId, MovieModificationRequest request, CancellationToken cancellationToken)
        {
            var httpResponse = await MakeCommandRequest<MovieModificationRequest>(HttpMethod.Put,
                                                                            $"api/Account/{accountId}/Library/Media/Movies",
                                                                            request,
                                                                            cancellationToken);

            return await GetCommandResponse(httpResponse, cancellationToken);
        }

        public async Task<CommandResponseStatus> DeleteAsync(int accountId, int movieId, CancellationToken cancellationToken)
        {
            var httpResponse = await MakeCommandRequest<MovieCreationRequest>(HttpMethod.Delete,
                                                                            $"api/Account/{accountId}/Library/Media/Movies/{movieId}",
                                                                            body: null,
                                                                            cancellationToken);

            return await GetCommandResponse(httpResponse, cancellationToken);
        }

        public async Task<Movie> GetAsync(int accountId, int movieId, CancellationToken cancellationToken)
        {
            var httpResponse = await MakeQueryRequest($"api/Account/{accountId}/Library/Media/Movies/{movieId}",
                                                      cancellationToken);

            return await GetQueryResponse<Movie>(httpResponse, cancellationToken);
        }

        public async Task<List<Movie>> GetAsync(int accountId, CancellationToken cancellationToken)
        {
            var httpResponse = await MakeQueryRequest($"api/Account/{accountId}/Library/Media/Movies",
                                                      cancellationToken);

            return await GetQueryResponse<List<Movie>>(httpResponse, cancellationToken);
        }
    }
}
