using AutoMapper;
using Library.UI.Adapters;
using Library.UI.Model.ViewModels.Media;
using Library.UI.Utilities;
using Microsoft.AspNetCore.Components;
using ViewModels = Library.UI.Model.ViewModels;
using Adapter = Library.Models.Media;
using Microsoft.JSInterop;
using AutoMapper.Internal.Mappers;

namespace Library.UI.Components.Media
{
    public partial class NewCollection
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
        protected IMapper Mapper { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager Navigation {  get; set; }

        private ViewModels.Media.NewCollection newCollection = new();
        private Utilities.NotificationUtility notificationUtility;
        private string searchTerm = "";
        private bool loading = true;

        protected override void OnInitialized()
        {
            newCollection.Name = "New Collection";
            base.OnInitialized();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                notificationUtility = new NotificationUtility(JSRuntime);
                await GetAllContentAsync();
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task GetAllContentAsync()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            newCollection.Books = Mapper.Map<List<NewCollectionContent>>(await BookAdapter.GetAsync(Utilities.Account.AccountId, cts.Token));
            newCollection.Music = Mapper.Map<List<NewCollectionContent>>(await MusicAdapter.GetAsync(Utilities.Account.AccountId, cts.Token));
            newCollection.Movies = Mapper.Map<List<NewCollectionContent>>(await MovieAdapter.GetAsync(Utilities.Account.AccountId, cts.Token));
            newCollection.Collections = Mapper.Map<List<NewCollectionContent>>(await CollectionAdapter.GetAsync(Utilities.Account.AccountId, cts.Token));
            loading = false;
            cts.Dispose();
            StateHasChanged();
        }

        private void OnSearch(string searchCriteria)
        {
            searchTerm = searchCriteria;
        }

        private void OnToggleContentSelection(MediaType? mediaType, int id)
        {
            if(mediaType == MediaType.Book)
            {
                var element = newCollection.Books.FirstOrDefault(f => f.Id == id);
                element!.Selected = !element.Selected;
            }
            else if(mediaType == MediaType.Music)
            {
                var element = newCollection.Music.FirstOrDefault(f => f.Id == id);
                element!.Selected = !element.Selected;
            }
            else if(mediaType == MediaType.Movie)
            {
                var element = newCollection.Movies.FirstOrDefault(f => f.Id == id);
                element!.Selected = !element.Selected;
            }
            else 
            {
                var element = newCollection.Collections.FirstOrDefault(f => f.Id == id);
                element!.Selected = !element.Selected;
            }
        }

        private async Task OnSaveCollection()
        {
            var collection = new Adapter.CollectionCreationRequest()
            {
                Title = newCollection.Name,
                NewCollectionContents = new()
            };
            collection.NewCollectionContents.AddRange(newCollection.Books.Where(w => w.Selected).Select(s => new Adapter.NewCollectionContent(){MediaType = Adapter.MediaContentType.Book, Id = s.Id}));
            collection.NewCollectionContents.AddRange(newCollection.Music.Where(w => w.Selected).Select(s => new Adapter.NewCollectionContent(){MediaType = Adapter.MediaContentType.Music, Id = s.Id}));
            collection.NewCollectionContents.AddRange(newCollection.Movies.Where(w => w.Selected).Select(s => new Adapter.NewCollectionContent(){MediaType = Adapter.MediaContentType.Movie, Id = s.Id}));
            collection.NewCollectionContents.AddRange(newCollection.Collections.Where(w => w.Selected).Select(s => new Adapter.NewCollectionContent() {Id = s.Id}));

            using var command = new CommandUtility();
            await command.ExecuteAsync((request, token) => CollectionAdapter.CreateAsync(Utilities.Account.AccountId, request, token),
                                        collection,
                                        onSuccessId: OnSuccessSubmit,
                                        onFailureMessage: OnFailedSubmit);
        }

        private void OnSuccessSubmit(int collectionId)
        {
            Navigation.NavigateTo($"/collection/{collectionId}");
        }

        private void OnFailedSubmit(List<string> messages)
        {
            if (!messages.Any() || messages.All(a => String.IsNullOrWhiteSpace(a)))
            {
                messages.Add("Sorry, we had a problem saving all the information.");
            }
            notificationUtility.ShowNotification("Failed Collection Creation", messages);
        }
    }
}