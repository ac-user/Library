using AutoMapper;
using Library.Data;
using Model = Library.Services.Models;
using Entity = Library.Data.Entities;
using Library.Services.Models.Media;

namespace Library.Services.Commands
{
    public class CollectionCommand : ICollectionCommand
    {
        private readonly LibraryContext _context;
        private IMapper _mapper;

        public CollectionCommand(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(Collection newItem, CancellationToken cancellationToken)
        {
            var collection = new Entity.Collection() { Title = newItem.Name };
            await _context.Collections.AddAsync(collection, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return collection.CollectionId;
        }
        public async Task<List<int>> CreateAsync(int collectionId, List<Model.Media.Book.Book> newItems, CancellationToken cancellationToken)
        {
            var collectionContent = new List<Entity.CollectionAssociation>();
            if (newItems != null && newItems.Any())
            {
                collectionContent.AddRange(newItems.Select(s => new Entity.CollectionAssociation()
                {
                    CollectionId = collectionId,
                    MediaId = s.Id,
                    MediaType = "Book"
                }));
            }
            
            if (collectionContent.Any())
            {
                await _context.CollectionAssociations.AddRangeAsync(collectionContent, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return collectionContent.Select(s => s.CollectionAssociationId).ToList();
        }
        public async Task<List<int>> CreateAsync(int collectionId, List<Model.Media.Music.Music> newItems, CancellationToken cancellationToken)
        {
            var collectionContent = new List<Entity.CollectionAssociation>();
            if (newItems != null && newItems.Any())
            {
                collectionContent.AddRange(newItems.Select(s => new Entity.CollectionAssociation()
                {
                    CollectionId = collectionId,
                    MediaId = s.Id,
                    MediaType = "Music"
                }));
            }
            
            if (collectionContent.Any())
            {
                await _context.CollectionAssociations.AddRangeAsync(collectionContent, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return collectionContent.Select(s => s.CollectionAssociationId).ToList();
        }
        public async Task<List<int>> CreateAsync(int collectionId, List<Model.Media.Movies.Movie> newItems, CancellationToken cancellationToken)
        {
            var collectionContent = new List<Entity.CollectionAssociation>();
            if (newItems != null && newItems.Any())
            {
                collectionContent.AddRange(newItems.Select(s => new Entity.CollectionAssociation()
                {
                    CollectionId = collectionId,
                    MediaId = s.Id,
                    MediaType = "Movie"
                }));
            }
            
            if (collectionContent.Any())
            {
                await _context.CollectionAssociations.AddRangeAsync(collectionContent, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return collectionContent.Select(s => s.CollectionAssociationId).ToList();
        }
        public async Task<List<int>> CreateAsync(int collectionId, List<Collection> newItems, CancellationToken cancellationToken)
        {
            var collectionContent = new List<Entity.SubCollectionAssociation>();
            if (newItems != null && newItems.Any())
            {
                collectionContent.AddRange(newItems.Select(s => new Entity.SubCollectionAssociation()
                {
                    CollectionId = collectionId,
                    SubCollectionId = s.Id
                }));
            }
            
            if (collectionContent.Any())
            {
                await _context.SubCollectionAssociations.AddRangeAsync(collectionContent, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return collectionContent.Select(s => s.SubCollectionAssociationId).ToList();
        }

        public async Task<bool> UpdateAsync(Collection item, CancellationToken cancellationToken)
        {
            var itemToModify = _context.Collections.FirstOrDefault(f => f.CollectionId == item.Id);
            bool success = true;

            if (itemToModify != null)
            {
                itemToModify.Title = item.Name;
                _context.Collections.Update(itemToModify);
                success = await _context.SaveChangesAsync(cancellationToken) == 1;
            }

            return success;
        }

        public async Task<bool> DeleteAsync(int itemId, CancellationToken cancellationToken)
        {//TODO: include sub items to delete and cascade delete for data
            var itemToDelete = _context.Collections.FirstOrDefault(f => f.CollectionId == itemId);
            bool success = true;

            if (itemToDelete != null)
            {
                _context.Collections.Remove(itemToDelete);
                success = await _context.SaveChangesAsync(cancellationToken) == 1;
            }

            return success;
        }
        public async Task<bool> DeleteAsync(int collectionId, int itemId, CancellationToken cancellationToken)
        {
            var itemToDelete = _context.CollectionAssociations.FirstOrDefault(f => f.CollectionId == collectionId && f.MediaId == itemId);
            bool success = true;

            if (itemToDelete != null)
            {
                _context.CollectionAssociations.Remove(itemToDelete);
                success = await _context.SaveChangesAsync(cancellationToken) == 1;
            }

            return success;
        }

        public async Task<bool> DeleteAllAsync(int accountId, CancellationToken cancellationToken)
        {//TODO: include sub items to delete
            var itemsToDelete = _context.Collections.Where(f => f.CollectionId == accountId);
            bool success = true;

            if (itemsToDelete != null)
            {
                _context.Collections.RemoveRange(itemsToDelete);
                success = await _context.SaveChangesAsync(cancellationToken) == itemsToDelete.Count();
            }

            return success;
        }

    }
}
