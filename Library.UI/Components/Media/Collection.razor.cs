using AutoMapper;
using Library.Models.Media;
using Library.UI.Adapters;
using Library.UI.Model.ViewModels.Media;
using Library.UI.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ViewModels = Library.UI.Model.ViewModels.Media;

namespace Library.UI.Components.Media
{
    public partial class Collection
    {
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
        private string searchTerm = "";
        private bool showMedia;
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
                .. result.Books.Select(s => new ViewModels.Media() { Id = s.Id, Title = s.Title, Type = MediaType.Book }),
                .. result.Music.Select(s => new ViewModels.Media() { Id = s.Id, Title = s.Title, Type = MediaType.Music }),
                .. result.Movies.Select(s => new ViewModels.Media() { Id = s.Id, Title = s.Title, Type = MediaType.Movie }),
            ];
            cts.Dispose();
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

        private void OnSuccessSubmit(int id, MediaType mediaType)
        {
            collection.MediaContent.Remove(collection.MediaContent.First(f => f.Id == id && f.Type == mediaType));
            
            notificationUtility.ShowNotification("Success", "Removed content from collection");
        }

        private void OnFailedSubmit()
        {
            notificationUtility.ShowNotification("Failed", "Failed to delete content");
        }


        private void OnClickAddCollection()
        {
            Navigation.NavigateTo($"/new-collection");
        }

        private void NavigateToCollection(int id)
        {
            Navigation.NavigateTo($"/collection/{id}");
        }

        private void OnClickMedia(int id, MediaType mediaType)
        {
            contentToOpen = id;
            contentType = mediaType;
            showMedia = true;
        }

        private void OnClickAddMedia()
        {
            addMedia = true;
        }

        private void CloseMediaPopup()
        {
            showMedia = false;
            addMedia = false;
        }

        private void OnSearch(string searchCriteria)
        {
            searchTerm = searchCriteria ?? "";
        }
    }
}