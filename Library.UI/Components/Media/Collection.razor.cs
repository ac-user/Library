using AutoMapper;
using Library.Models.Media;
using Library.UI.Adapters;
using Library.UI.Model.ViewModels.Media;
using Library.UI.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.JSInterop;
using ViewModels = Library.UI.Model.ViewModels.Media;

namespace Library.UI.Components.Media
{
    public partial class Collection
    {
        [Inject]
        protected IBookAdapter BookAdapter { get; set; }
        [Inject]
        protected IMusicAdapter MusicAdapter { get; set; }
        [Inject]
        protected IMovieAdapter MovieAdapter { get; set; }
        [Inject]
        protected ICollectionAdapter CollectionAdapter { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected IMapper Mapper { get; set; }

        [Inject]
        protected NavigationManager Navigation { get; set; }


        [Parameter]
        public int collectionId { get; set; }


        private NotificationUtility notificationUtility;
        private ViewModels.Collection collection;
        private List<ViewModels.NewCollectionContent> allMediaContent; 
        private List<ViewModels.SelectableCollection> allCollections; 
        private string searchTerm = "";
        private bool showMedia;
        private bool addCollection;
        private bool addMedia;
        private int contentToOpen;
        private MediaType contentType;

        protected override async Task OnParametersSetAsync()
        {
            await GetCollectionAsync();
            notificationUtility = new NotificationUtility(JSRuntime);
            await base.OnParametersSetAsync();
        }

        private async Task GetCollectionAsync()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var result = await CollectionAdapter.GetAsync(Utilities.Account.AccountId, collectionId, cts.Token);
            collection = Mapper.Map<ViewModels.Collection>(result);
            collection.MediaContent =
            [
                .. result.Books.Select(s => new ViewModels.Media() { Id = s.Id, Title = s.Title, Type = MediaType.Book, Image = s.Image}),
                .. result.Music.Select(s => new ViewModels.Media() { Id = s.Id, Title = s.Title, Type = MediaType.Music, Image = s.Image }),
                .. result.Movies.Select(s => new ViewModels.Media() { Id = s.Id, Title = s.Title, Type = MediaType.Movie, Image = s.Image }),
            ];
            cts.Dispose();
        }

        private async Task GetMediaContentAsync()
        {
            CancellationTokenSource cts = new CancellationTokenSource(); 
            var books = await BookAdapter.GetAsync(Utilities.Account.AccountId, cts.Token);
            var music = await MusicAdapter.GetAsync(Utilities.Account.AccountId, cts.Token);
            var movies = await MovieAdapter.GetAsync(Utilities.Account.AccountId, cts.Token);

            allMediaContent =
            [
                .. books.Select(s => new ViewModels.NewCollectionContent() { Id = s.Id, Title = s.Title, Type = MediaType.Book }),
                .. music.Select(s => new ViewModels.NewCollectionContent() { Id = s.Id, Title = s.Title, Type = MediaType.Music }),
                .. movies.Select(s => new ViewModels.NewCollectionContent() { Id = s.Id, Title = s.Title, Type = MediaType.Movie })
            ];

            cts.Dispose();
        }

        private async Task GetCollectionsAsync()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            allCollections = Mapper.Map<List<ViewModels.SelectableCollection>>(await CollectionAdapter.GetAsync(Utilities.Account.AccountId, cts.Token));
            cts.Dispose();
        }

        private async Task OnAssociateMediaContentClickAsync()
        {
            using var command = new CommandUtility();
            foreach (var content in allMediaContent.Where(w => w.Selected))
            {
                var type = Mapper.Map<MediaContentType>(content.Type);
                var tuple = (Utilities.Account.AccountId, collectionId, type, content.Id);

                await command.ExecuteAsync((tuple, token) => CollectionAdapter.CreateAsync(Utilities.Account.AccountId, collectionId, type, content.Id, new CancellationToken()),
                                        tuple,
                                        onSuccess: () => OnSuccessSubmit(),
                                        onFailure: OnFailedSubmit);
            }
            
        }

        private async Task OnAssociateSubCollectionClickAsync()
        {
            using var command = new CommandUtility();
            foreach (var content in allCollections.Where(w => w.Selected))
            {
                var tuple = (Utilities.Account.AccountId, collectionId, content.Id);

                await command.ExecuteAsync((tuple, token) => CollectionAdapter.CreateAsync(Utilities.Account.AccountId, collectionId, content.Id, new CancellationToken()),
                                        tuple,
                                        onSuccess: () => OnSuccessSubmit(),
                                        onFailure: OnFailedSubmit);
            }
        }

        private async Task OnDeleteClickAsync(int id, MediaType mediaType)
        {
            using var command = new CommandUtility();
            var type = Mapper.Map<MediaContentType>(mediaType);
            var tuple = (Utilities.Account.AccountId, collectionId, type, id);

            await command.ExecuteAsync((tuple, token) => CollectionAdapter.DeleteAsync(Utilities.Account.AccountId, collectionId, type, id, new CancellationToken()),
                                    tuple,
                                    onSuccess: () => OnSuccessSubmit(id, mediaType),
                                    onFailure: OnFailedSubmit);
            
        }

        private async Task OnDeleteClickAsync(int id)
        {
            using var command = new CommandUtility();
            var tuple = (Utilities.Account.AccountId, collectionId, id);

            await command.ExecuteAsync((tuple, token) => CollectionAdapter.DeleteAsync(Utilities.Account.AccountId, collectionId, id, new CancellationToken()),
                                    tuple,
                                    onSuccess: () => OnSuccessSubmit(id),
                                    onFailure: OnFailedSubmit);
            
        }

        private void OnSuccessSubmit(int id, MediaType mediaType)
        {
            collection.MediaContent.Remove(collection.MediaContent.First(f => f.Id == id && f.Type == mediaType));
            
            notificationUtility.ShowNotification("Success", "Removed content from collection");
        }

        private void OnSuccessSubmit(int id)
        {
            collection.SubCollections.Remove(collection.SubCollections.First(f => f.Id == id));
            
            notificationUtility.ShowNotification("Success", "Removed content from collection");
        }

        private void OnSuccessSubmit()
        {
            notificationUtility.ShowNotification("Success", "Associated new content");
            if(addCollection)
            {
                foreach(var content in allCollections.Where(w => w.Selected))
                {
                    collection.SubCollections.Add(new ViewModels.Collection() 
                    {
                        Id = content.Id,
                        Name = content.Title
                    });
                    content.Selected = false;
                }

            }
            if(addMedia)
            {
                foreach(var content in allMediaContent.Where(w => w.Selected))
                {
                    collection.MediaContent.Add(new ViewModels.Media() 
                    {
                        Id = content.Id,
                        Title = content.Title
                    });
                    content.Selected = false;
                }
            }
            addCollection = false;
            addMedia = false;
        }

        private void OnFailedSubmit()
        {
            notificationUtility.ShowNotification("Failed", "Failed to delete content");
        }

        private async Task OnClickAddCollectionAsync()
        {
            if (allCollections == null)
            {
                await GetCollectionsAsync();
            }
            addCollection = true;
        }

        private async Task OnClickAddMediaAsync()
        {
            if (allMediaContent == null)
            {
                await GetMediaContentAsync();
            }
            addMedia = true;
        }
        
        private void OnClickMedia(int id, MediaType mediaType)
        {
            contentToOpen = id;
            contentType = mediaType;
            showMedia = true;
        }

        private void CloseMediaPopup()
        {
            showMedia = false;
            addMedia = false;
            addCollection=false;
        }

        private void ToggleSelection(int id)
        {
            var content = allCollections.First(f => f.Id == id);
            content.Selected = !content.Selected;
        }
        
        private void ToggleSelection(int id, MediaType type)
        {
            var content = allMediaContent.First(f => f.Id == id && f.Type == type);
            content.Selected = !content.Selected;
        }

        private void NavigateToCollection(int id)
        {
            Navigation.NavigateTo($"/collection/{id}");
        }

        private void OnSearch(string searchCriteria)
        {
            searchTerm = searchCriteria ?? "";
        }
    }
}