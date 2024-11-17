using Library.Services.Commands;
using Library.Services.Models;
using Library.Services.Models.Media.Music;
using Library.Services.Queries;

namespace Library.Services.Services.Media
{
    public class MusicService : IContentServiceFactory<Music>
    {
        private readonly IContentCommandFactory<Music> _command;
        private readonly IContentQueryFactory<Music> _query;

        public MusicService(IContentCommandFactory<Music> command, IContentQueryFactory<Music> query)
        {
            _command = command;
            _query = query;
        }

        public async Task<Music> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _query.GetAsync(id, cancellationToken);
        }

        public async Task<List<Music>> GetAllAsync(int accountId, CancellationToken cancellationToken)
        {
            return await _query.GetAllAsync(accountId, cancellationToken);
        }

        public async Task<ResponseStatus> CreateAsync(Music item, CancellationToken cancellationToken)
        {
            int id = await _command.CreateAsync(item, cancellationToken);
            var response = new ResponseStatus()
            {
                Id = id,
                IsSuccess = (id != 0),
            };

            if (!response.IsSuccess)
            {
                response.Messages = new List<string>()
                {
                    "Had problem adding the music."
                };
            }

            return response;
        }

        public async Task<ResponseStatus> UpdateAsync(Music item, CancellationToken cancellationToken)
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
                    "Had problem deleting the music."
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
                    "Had problem deleting all music from the account."
                };
            }

            return response;
        }

    }
}
