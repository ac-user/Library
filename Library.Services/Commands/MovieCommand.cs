using AutoMapper;
using Model = Library.Services.Models.Media.Movies;
using Entity = Library.Data.Entities;
using Library.Data;

namespace Library.Services.Commands
{
    public class MovieCommand : IContentCommandFactory<Model.Movie>
    {
        private readonly LibraryContext _context;
        private IMapper _mapper;

        public MovieCommand(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(Model.Movie newItem, CancellationToken cancellationToken)
        {
            var newEntity = _mapper.Map<Entity.Movie>(newItem);
            await _context.Movies.AddAsync(newEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return newEntity.MovieId;
        }

        public async Task<bool> UpdateAsync(Model.Movie item, CancellationToken cancellationToken)
        {
            var itemToModify = _mapper.Map<Entity.Movie>(item);
            bool success = true;

            if (itemToModify != null)
            {
                _context.Movies.Update(itemToModify);
                success = await _context.SaveChangesAsync(cancellationToken) == 1;
            }

            return success;
        }

        public async Task<bool> DeleteAsync(int itemId, CancellationToken cancellationToken)
        {
            var itemToDelete = _context.Movies.FirstOrDefault(f => f.MovieId == itemId);
            bool success = true;

            if (itemToDelete != null)
            {
                _context.Movies.Remove(itemToDelete);
                success = await _context.SaveChangesAsync(cancellationToken) == 1;
            }

            return success;
        }

        public async Task<bool> DeleteAllAsync(int accountId, CancellationToken cancellationToken)
        {
            var itemsToDelete = _context.Movies.Where(f => f.MovieId == accountId);
            bool success = true;

            if (itemsToDelete != null)
            {
                _context.Movies.RemoveRange(itemsToDelete);
                success = await _context.SaveChangesAsync(cancellationToken) == itemsToDelete.Count();
            }

            return success;
        }
    }
}
