using AutoMapper;
using Model = Library.Models.Media.Book;
using Entity = Library.Data.Entities;
using Library.Data;
using Microsoft.Identity.Client;

namespace Library.Services.Commands
{
    public class BookCommand : IContentCommandFactory<Model.Book>
    {
        private readonly LibraryContext _context;
        private IMapper _mapper;

        public BookCommand(LibraryContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(int accountId, Model.Book newItem, CancellationToken cancellationToken)
        {
            var newEntity = _mapper.Map<Entity.Book>(newItem);
            newEntity.AccountId = accountId;
            await _context.Books.AddAsync(newEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return newEntity.BookId; 
        }

        public async Task<bool> UpdateAsync(int accountId, Model.Book item, CancellationToken cancellationToken)
        {
            var itemToModify = _mapper.Map<Entity.Book>(item);
            itemToModify.AccountId = accountId;
            bool success = true;
            
            if(itemToModify != null)
            {
                _context.Books.Update(itemToModify);
                success = await _context.SaveChangesAsync(cancellationToken) == 1;
            }

            return success; 
        }

        public async Task<bool> DeleteAsync(int itemId, CancellationToken cancellationToken)
        {
            var itemToDelete = _context.Books.FirstOrDefault(f => f.BookId == itemId);
            bool success = true;
            
            if(itemToDelete != null)
            {
                _context.Books.Remove(itemToDelete);
                success = await _context.SaveChangesAsync(cancellationToken) == 1;
            }

            return success; 
        }

        public async Task<bool> DeleteAllAsync(int accountId, CancellationToken cancellationToken)
        {
            var itemsToDelete = _context.Books.Where(f => f.AccountId == accountId);
            bool success = true;
            
            if(itemsToDelete != null)
            {
                _context.Books.RemoveRange(itemsToDelete);
                success = await _context.SaveChangesAsync(cancellationToken) == itemsToDelete.Count();
            }

            return success; 
        }
    }
}
