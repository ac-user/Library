using Library.Services.Commands;
using Library.Services.Models;
using Library.Models.Media;
using Library.Services.Queries;
using AutoMapper;
using Microsoft.Identity.Client;

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

        private bool DetermineIfSubCollectionContainsCollectionId(int collectionIdToFind, List<Collection> subCollection)
        {
            bool found = false;
            for(int index = 0; subCollection != null && index < subCollection.Count && !found; index++)
            {
                found = subCollection[index].Id == collectionIdToFind;
                if(!found && subCollection[index].SubCollections.Any())
                {
                    found = DetermineIfSubCollectionContainsCollectionId(collectionIdToFind , subCollection[index].SubCollections);
                }
            }
            return found;
        }

        public async Task<ResponseStatus> CreateAsync(int collectionId, int subId, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus();
            bool isCollectionIdASubCollection = collectionId == subId;
            
            if(!isCollectionIdASubCollection)
            {
                var allSubCollectionsOfPotentialSubCollection = await GetAsync(subId, cancellationToken);
                isCollectionIdASubCollection = DetermineIfSubCollectionContainsCollectionId(collectionId, allSubCollectionsOfPotentialSubCollection.SubCollections);
                if (!isCollectionIdASubCollection)
                {
                    var id = await _command.CreateAsync(collectionId, new List<Collection>() { new Collection() { Id = subId } }, cancellationToken);
                    response = new ResponseStatus()
                    {
                        Id = id[0],
                        IsSuccess = (id[0] != 0),
                        Messages = new List<string>()
                    };

                    if (!response.IsSuccess)
                    {
                        response.Messages.Add("Had problem associating subcollection");
                    }
                }
            }

            if (isCollectionIdASubCollection)
            {
                response.Messages = new List<string>() { "Cannot associate a subcollection that is a parent to the collection" };
            }

            return response;
        }

        public async Task<ResponseStatus> CreateAsync(int collectionId, MediaContentType mediaType, int mediaId, CancellationToken cancellationToken)
        {
            var media = new List<CollectionContentAssociation>()
            {
                new CollectionContentAssociation() 
                { 
                    MediaId = mediaId,
                    MediaType = mediaType
                }
            };

            var mediaIds = await _command.CreateAsync(collectionId, media, cancellationToken);
            var response = new ResponseStatus()
            {
                Id = mediaIds[0],
                IsSuccess = (mediaIds[0] != 0),
                Messages = new List<string>()
            };

            if (!response.IsSuccess)
            {
                response.Messages.Add("Had problem adding media to the collection.");
            }
            return response;
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
                var media = new List<CollectionContentAssociation>();
                media.AddRange(item.Books.Select(s => new CollectionContentAssociation() { MediaId = s.Id, MediaType = MediaContentType.Book}));
                media.AddRange(item.Music.Select(s => new CollectionContentAssociation() { MediaId = s.Id, MediaType = MediaContentType.Music}));
                media.AddRange(item.Movies.Select(s => new CollectionContentAssociation() { MediaId = s.Id, MediaType = MediaContentType.Movie}));

                var mediaIds = await _command.CreateAsync(id, media, cancellationToken);

                if(mediaIds.Any(a => a == 0))
                {
                    response.Messages.Add("Had problem adding some media to the collection.");
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

        public async Task<ResponseStatus> DeleteAsync(int collectionId, MediaContentType mediaType, int mediaId, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus()
            {
                IsSuccess = await _command.DeleteAsync(collectionId, mediaType, mediaId, cancellationToken)
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
        public async Task<ResponseStatus> DeleteAsync(int collectionId, int subId, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus()
            {
                IsSuccess = await _command.DeleteAsync(collectionId, subId, cancellationToken)
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
