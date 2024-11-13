using Library.Services.Models;
using Library.Services.Models.Media.Movies;

namespace Library.Services.Services.Media
{
    public class MovieService : IContentServiceFactory<Movie>
    {

        public MovieService() { }

        public async Task<List<Movie>> GetAllAsync(int accountId, CancellationToken cancellationToken)
        {
            return new List<Movie>();
        }

        public async Task<Movie> GetAsync(int id, CancellationToken cancellationToken)
        {
            return new Movie();
        }

        public async Task<Movie> GetAsync(int accountId, string creator, CancellationToken cancellationToken)
        {
            return new Movie();
        }

        public async Task<ResponseStatus> CreateAsync(Movie item, CancellationToken cancellationToken)
        {
            return new ResponseStatus();
        }


        public async Task<ResponseStatus> UpdateAsync(Movie item, CancellationToken cancellationToken)
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
