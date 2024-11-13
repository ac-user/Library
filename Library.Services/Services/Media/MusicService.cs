using Library.Services.Models;
using Library.Services.Models.Media.Music;

namespace Library.Services.Services.Media
{
    public class MusicService : IContentServiceFactory<Music>
    {
        public MusicService() { }


        public async Task<List<Music>> GetAllAsync(int accountId, CancellationToken cancellationToken)
        {
            return new List<Music>();
        }

        public async Task<Music> GetAsync(int id, CancellationToken cancellationToken)
        {
            return new Music();
        }

        public async Task<Music> GetAsync(int accountId, string creator, CancellationToken cancellationToken)
        {
            return new Music();
        }

        public async Task<ResponseStatus> CreateAsync(Music item, CancellationToken cancellationToken)
        {
            return new ResponseStatus();
        }


        public async Task<ResponseStatus> UpdateAsync(Music item, CancellationToken cancellationToken)
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
