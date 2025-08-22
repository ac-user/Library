using AutoMapper;
using Library.Data;
using Library.Models.Media;
using Library.Models.Media.Book;
using Library.Models.Media.Movies;
using Library.Models.Media.Music;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Queries
{
    public class CollectionQuery : IContentQueryFactory<Collection>
    {
        private readonly LibraryContext _context;
        private IMapper _mapper;

        public CollectionQuery(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Collection> GetAsync(int itemId, CancellationToken cancellationToken)
        {
            var result = new Collection();
            var collection = await _context.Collections.AsNoTracking().FirstOrDefaultAsync(f => f.CollectionId == itemId, cancellationToken);
            if(collection != null)
            {
                result.Id = itemId;

                /*Gather media content*/
                var books = _mapper.Map<List<Book>>(await _context.CollectionAssociations.AsNoTracking().Include(i => i.Book)
                                                           .Where(w => w.CollectionId == itemId && w.MediaType == MediaContentType.Book.ToString())
                                                           .Select(s => s.Book).ToListAsync(cancellationToken));
                var music = _mapper.Map<List<Music>>(await _context.CollectionAssociations.AsNoTracking().Include(i => i.Music)
                                                           .Where(w => w.CollectionId == itemId && w.MediaType == MediaContentType.Music.ToString())
                                                           .Select(s => s.Music).ToListAsync(cancellationToken));
                var movies = _mapper.Map<List<Movie>>(await _context.CollectionAssociations.AsNoTracking().Include(i => i.Movie)
                                                           .Where(w => w.CollectionId == itemId && w.MediaType == MediaContentType.Movie.ToString())
                                                           .Select(s => s.Movie).ToListAsync(cancellationToken));
                result.Name = collection!.Title;
                result.Books = books;
                result.Music = music;
                result.Movies = movies;                

                /*Gather collection content*/
                var subCollectionIds = await _context.SubCollectionAssociations.AsNoTracking().Where(w => w.CollectionId == itemId)
                                                                         .Select(s => s.SubCollectionId).ToListAsync(cancellationToken);
                result.SubCollections = new();
                foreach (var subCollectionId in subCollectionIds)
                {
                    result.SubCollections.Add(await GetAsync(subCollectionId, cancellationToken));
                }
            }
            return result;
        }

        public async Task<List<Collection>> GetAllAsync(int accountId, CancellationToken cancellationToken)
        {
            var result = new List<Collection>();
            var collections = await _context.Collections.AsNoTracking().Where(w => w.AccountId == accountId).ToListAsync(cancellationToken);
            if (collections != null)
            {
                foreach(var collection in collections)
                {                   
                    result.Add(await GetAsync(collection.CollectionId, cancellationToken));
                }
            }
            return result;
        }
    }
}
