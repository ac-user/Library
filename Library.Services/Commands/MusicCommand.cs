using AutoMapper;
using Model = Library.Services.Models.Media.Music;
using Entity = Library.Data.Entities;
using Library.Data;

namespace Library.Services.Commands
{
    public class MusicCommand : IContentCommandFactory<Model.Music>
    {
        private readonly LibraryContext _context;
        private IMapper _mapper;

        public MusicCommand(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(Model.Music newItem, CancellationToken cancellationToken)
        {
            var newEntity = _mapper.Map<Entity.Music>(newItem);
            await _context.Musics.AddAsync(newEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return newEntity.MusicId;
        }

        public async Task<bool> UpdateAsync(Model.Music item, CancellationToken cancellationToken)
        {
            var itemToModify = _mapper.Map<Entity.Music>(item);
            bool success = true;

            if (itemToModify != null)
            {
                _context.Musics.Update(itemToModify);
                success = await _context.SaveChangesAsync(cancellationToken) == 1;
            }

            return success;
        }

        public async Task<bool> DeleteAsync(int itemId, CancellationToken cancellationToken)
        {
            var itemToDelete = _context.Musics.FirstOrDefault(f => f.MusicId == itemId);
            bool success = true;

            if (itemToDelete != null)
            {
                _context.Musics.Remove(itemToDelete);
                success = await _context.SaveChangesAsync(cancellationToken) == 1;
            }

            return success;
        }

        public async Task<bool> DeleteAllAsync(int accountId, CancellationToken cancellationToken)
        {
            var itemsToDelete = _context.Musics.Where(f => f.MusicId == accountId);
            bool success = true;

            if (itemsToDelete != null)
            {
                _context.Musics.RemoveRange(itemsToDelete);
                success = await _context.SaveChangesAsync(cancellationToken) == itemsToDelete.Count();
            }

            return success;
        }
    }
}
