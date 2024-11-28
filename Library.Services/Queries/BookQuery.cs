using AutoMapper;
using Library.Data;
using Library.Models.Media.Book;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Queries
{
    public class BookQuery : IContentQueryFactory<Book>
    {
        private readonly LibraryContext _context;
        private IMapper _mapper;

        public BookQuery(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Book> GetAsync(int itemId, CancellationToken cancellationToken)
        {
            return _mapper.Map<Book>(await _context.Books.FirstOrDefaultAsync(f => f.BookId == itemId, cancellationToken));
        }

        public async Task<List<Book>> GetAllAsync(int accountId, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<Book>>(await _context.Books.Where(f => f.AccountId == accountId).ToListAsync(cancellationToken));
        }
    }
}
