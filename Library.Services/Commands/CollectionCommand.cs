using AutoMapper;
using Library.Data;
using Model = Library.Models;
using Entity = Library.Data.Entities;
using Library.Models.Media;
using Microsoft.EntityFrameworkCore;
using Library.Services.Models;

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

        public async Task<int> CreateAsync(int accountId, Model.Media.Collection newItem, CancellationToken cancellationToken)
        {
            var collection = new Entity.Collection() 
            { 
                Title = newItem.Name,
                AccountId = accountId
            };
            await _context.Collections.AddAsync(collection, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return collection.CollectionId;
        }
        public async Task<List<int>> CreateAsync(int collectionId, List<CollectionContentAssociation> newItems, CancellationToken cancellationToken)
        {
            if (newItems != null && newItems.Any())
            {
                var collectionContent = newItems.Select(s => new Entity.CollectionAssociation()
                {
                    CollectionId = collectionId,
                    MediaId = s.MediaId,
                    MediaType = s.MediaType.ToString()
                });

                _context.CollectionAssociations.AddRange(collectionContent);
                await _context.SaveChangesAsync(cancellationToken);
            
                return collectionContent.Select(s => s.CollectionAssociationId).ToList();
            }
            
            return new List<int>();
        }

        public async Task<List<int>> CreateAsync(int collectionId, List<Model.Media.Collection> newItems, CancellationToken cancellationToken)
        {
            if (newItems != null && newItems.Any())
            {
                var collectionContent = newItems.Select(s => new Entity.SubCollectionAssociation()
                {
                    CollectionId = collectionId,
                    SubCollectionId = s.Id
                });

                await _context.SubCollectionAssociations.AddRangeAsync(collectionContent, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                
                return collectionContent.Select(s => s.SubCollectionAssociationId).ToList();
            }
            return new List<int>();
        }

        public async Task<bool> UpdateAsync(int accountId, Model.Media.Collection item, CancellationToken cancellationToken)
        {
            var itemToModify = _context.Collections.FirstOrDefault(f => f.CollectionId == item.Id);
            bool success = true;

            if (itemToModify != null && itemToModify.AccountId == accountId)
            {
                itemToModify.Title = item.Name;
                _context.Collections.Update(itemToModify);
                success = await _context.SaveChangesAsync(cancellationToken) == 1;
            }

            return success;
        }


        public async Task<bool> DeleteAsync(int itemId, CancellationToken cancellationToken)
        {
            var itemToDelete = _context.Collections.Include(i => i.SubCollectionAssociations).FirstOrDefault(f => f.CollectionId == itemId);
            bool success = true;

            if (itemToDelete != null)
            {
                _context.Collections.Remove(itemToDelete);
                success = await _context.SaveChangesAsync(cancellationToken) >= 1;
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
        {
            var itemsToDelete = _context.Collections.Include(i => i.SubCollectionAssociations)
                                                    .Include(i => i.CollectionAssociations)
                                                    .Where(f => f.AccountId == accountId);
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
