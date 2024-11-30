using Library.Services.Commands;
using Library.Services.Models;
using Library.Models.Media;
using Library.Services.Queries;

namespace Library.Services.Services.Media
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionCommand _command;
        private readonly IContentQueryFactory<Collection> _query;

        public CollectionService(ICollectionCommand command, IContentQueryFactory<Collection> query)
        {
            _command = command;
            _query = query;
        }

        public async Task<Collection> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await _query.GetAsync(id, cancellationToken);
        }

        public async Task<List<Collection>> GetAllAsync(int accountId, CancellationToken cancellationToken)
        {
            return await _query.GetAllAsync(accountId, cancellationToken);
        }

        public async Task<ResponseStatus> CreateAsync(int accountId, Collection item, CancellationToken cancellationToken)
        {
            int id = await _command.CreateAsync(accountId ,item, cancellationToken);
            var response = new ResponseStatus()
            {
                Id = id,
                IsSuccess = (id != 0),
                Messages = new List<string>()
            };

            if (!response.IsSuccess)
            {
                response.Messages.Add("Had problem adding the collection.");
            }
            else
            {
                var bookIds = await _command.CreateAsync(id, item.Books, cancellationToken);
                var musicIds = await _command.CreateAsync(id, item.Music, cancellationToken);
                var movieIds = await _command.CreateAsync(id, item.Movies, cancellationToken);

                if(bookIds.Any(a => a == 0))
                {
                    response.Messages.Add("Had problem adding some books to the collection.");
                    response.IsSuccess = false;
                }
                if(musicIds.Any(a => a == 0))
                {
                    response.Messages.Add("Had problem adding some music to the collection.");
                    response.IsSuccess = false;
                }
                if (movieIds.Any(a => a == 0))
                {
                    response.Messages.Add("Had problem adding some movies to the collection.");
                    response.IsSuccess = false;
                }

                var subCollectionIds = await _command.CreateAsync(id, item.SubCollections, cancellationToken);
                if (subCollectionIds.Any(a => a == 0))
                {
                    response.Messages.Add("Had problem adding some sub-collections to the collection.");
                    response.IsSuccess = false;
                }
            }


            return response;
        }

        public async Task<ResponseStatus> UpdateAsync(int accountId, Collection item, CancellationToken cancellationToken)
        {

            return new ResponseStatus();
        }

        public async Task<ResponseStatus> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus()
            {
                IsSuccess = await _command.DeleteAsync(id, cancellationToken)
            };

            if (!response.IsSuccess)
            {
                response.Messages = new List<string>()
                {
                    "Had problem deleting the collection."
                };
            }

            return response;
        }
        public async Task<ResponseStatus> DeleteAsync(int id, int collectionId, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus()
            {
                IsSuccess = await _command.DeleteAsync(id, collectionId, cancellationToken)
            };

            if (!response.IsSuccess)
            {
                response.Messages = new List<string>()
                {
                    "Had problem deleting the association to collection."
                };
            }

            return response;
        }

        public async Task<ResponseStatus> DeleteAllAsync(int accountId, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus()
            {
                IsSuccess = await _command.DeleteAllAsync(accountId, cancellationToken)
            };

            if (!response.IsSuccess)
            {
                response.Messages = new List<string>()
                {
                    "Had problem deleting all books from the account."
                };
            }

            return response;
        }

    }
}
