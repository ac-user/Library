using Library.Services.Commands;
using Library.Services.Models;
using Library.Models.Media.Movies;
using Library.Services.Queries;

namespace Library.Services.Services.Media
{
    public class MovieService : IContentServiceFactory<Movie>
    {
        private readonly IContentCommandFactory<Movie> _command;
        private readonly IContentQueryFactory<Movie> _query;

        public MovieService(IContentCommandFactory<Movie> command, IContentQueryFactory<Movie> query)
        {
            _command = command;
            _query = query;
        }

        public async Task<Movie> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _query.GetAsync(id, cancellationToken);
        }

        public async Task<List<Movie>> GetAllAsync(int accountId, CancellationToken cancellationToken)
        {
            return await _query.GetAllAsync(accountId, cancellationToken);
        }

        public async Task<ResponseStatus> CreateAsync(int accountId, Movie item, CancellationToken cancellationToken)
        {
            int id = await _command.CreateAsync(accountId, item, cancellationToken);
            var response = new ResponseStatus()
            {
                Id = id,
                IsSuccess = (id != 0),
            };

            if (!response.IsSuccess)
            {
                response.Messages = new List<string>()
                {
                    "Had problem adding the movie."
                };
            }

            return response;
        }

        public async Task<ResponseStatus> UpdateAsync(int accountId, Movie item, CancellationToken cancellationToken)
        {

            return new ResponseStatus();
        }

        public async Task<ResponseStatus> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus()
            {
                IsSuccess = await _command.DeleteAsync(id, cancellationToken)
            };

            if (!response.IsSuccess)
            {
                response.Messages = new List<string>()
                {
                    "Had problem deleting the movie."
                };
            }

            return response;
        }

        public async Task<ResponseStatus> DeleteAllAsync(int accountId, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus()
            {
                IsSuccess = await _command.DeleteAllAsync(accountId, cancellationToken)
            };

            if (!response.IsSuccess)
            {
                response.Messages = new List<string>()
                {
                    "Had problem deleting all movies from the account."
                };
            }

            return response;
        }

    }
}
