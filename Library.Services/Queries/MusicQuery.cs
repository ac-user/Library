using AutoMapper;
using Library.Data;
using Library.Services.Models.Media.Music;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Queries
{
    public class MusicQuery : IContentQueryFactory<Music>
    {
        private readonly LibraryContext _context;
        private IMapper _mapper;

        public MusicQuery(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Music> GetAsync(int itemId, CancellationToken cancellationToken)
        {
            return _mapper.Map<Music>(await _context.Musics.FirstOrDefaultAsync(f => f.MusicId == itemId, cancellationToken));
        }

        public async Task<List<Music>> GetAllAsync(int accountId, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<Music>>(await _context.Musics.Where(f => f.MusicId == accountId).ToListAsync(cancellationToken));
        }
    }
}
