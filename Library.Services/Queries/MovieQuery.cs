using AutoMapper;
using Library.Data;
using Library.Models.Media.Movies;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Queries
{
    public class MovieQuery : IContentQueryFactory<Movie>
    {
        private readonly LibraryContext _context;
        private IMapper _mapper;

        public MovieQuery(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Movie> GetAsync(int itemId, CancellationToken cancellationToken)
        {
            return _mapper.Map<Movie>(await _context.Movies.AsNoTracking().FirstOrDefaultAsync(f => f.MovieId == itemId, cancellationToken));
        }

        public async Task<List<Movie>> GetAllAsync(int accountId, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<Movie>>(await _context.Movies.AsNoTracking().Where(f => f.AccountId == accountId).ToListAsync(cancellationToken));
        }
    }
}
