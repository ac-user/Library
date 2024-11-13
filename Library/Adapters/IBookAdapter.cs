using Library.Models;
using Library.Models.Adapter;

namespace Library.Adapters
{
    public interface IBookAdapter
    {
        Task<CommandResponseStatus> CreateAsync(BookCreationRequest request, CancellationToken cancellationToken);
        Task<CommandResponseStatus> ModifyAsync(CancellationToken cancellationToken);
        Task<CommandResponseStatus> DeleteAsync(CancellationToken cancellationToken);
    }
}
