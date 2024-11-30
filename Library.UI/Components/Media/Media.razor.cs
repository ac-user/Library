using AutoMapper;
using Library.Models.Media;
using Library.UI.Adapters;
using Library.UI.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ViewModels = Library.UI.Model.ViewModels.Media;

namespace Library.UI.Components.Media
{
    public partial class Media
    {
        [Inject]
        protected IBookAdapter BookAdapter { get; set; }

        [Inject]
        protected IMusicAdapter MusicAdapter { get; set; }

        [Inject]
        protected IMovieAdapter MovieAdapter { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected IMapper Mapper { get; set; }

        [Parameter]
        public string mediaType { get; set; }

        private ViewModels.MediaType pageMediaType;
        private List<ViewModels.MediaCollection> mediaContents = new();
        private NotificationUtility notificationUtility;
        private bool cardView = true;
        private bool showDetails;
        private string searchTerm = "";
        private int contentToOpen;
        
        protected override void OnParametersSet()
        {
            pageMediaType = (ViewModels.MediaType)Enum.Parse(typeof(ViewModels.MediaType), mediaType);
            base.OnParametersSet();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetMediaContentsAsync();
                notificationUtility = new NotificationUtility(JSRuntime);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task GetMediaContentsAsync()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            if (pageMediaType == ViewModels.MediaType.Book)
            {
                mediaContents = Mapper.Map<List<ViewModels.MediaCollection>>(await BookAdapter.GetAsync(Utilities.Account.AccountId, cts.Token));
            }
            else if (pageMediaType == ViewModels.MediaType.Music)
            {
                mediaContents = Mapper.Map<List<ViewModels.MediaCollection>>(await MusicAdapter.GetAsync(Utilities.Account.AccountId, cts.Token));
            }
            else if (pageMediaType == ViewModels.MediaType.Movie)
            {
                mediaContents = Mapper.Map<List<ViewModels.MediaCollection>>(await MovieAdapter.GetAsync(Utilities.Account.AccountId, cts.Token));
            }
            cts.Dispose();
            StateHasChanged();
        }

        private async Task DeleteMediaContentAsync(int id)
        {
            using var command = new CommandUtility();
            var tuple = (Utilities.Account.AccountId, id);

            if (pageMediaType == ViewModels.MediaType.Book)
            {
                await command.ExecuteAsync((tuple, token) => BookAdapter.DeleteAsync(Utilities.Account.AccountId, id, new CancellationToken()),
                                        tuple,
                                        (() => OnSuccessSubmit(id)),
                                        OnFailedSubmit);
            }
            else if (pageMediaType == ViewModels.MediaType.Music)
            {
                await command.ExecuteAsync((tuple, token) => MusicAdapter.DeleteAsync(Utilities.Account.AccountId, id, token),
                                        tuple,
                                        (() => OnSuccessSubmit(id)),
                                        OnFailedSubmit);
            }
            else if (pageMediaType == ViewModels.MediaType.Movie)
            {
                await command.ExecuteAsync((tuple, token) => MovieAdapter.DeleteAsync(Utilities.Account.AccountId, id, token),
                                        tuple,
                                        (() => OnSuccessSubmit(id)),
                                        OnFailedSubmit);
            }
        }
        
        private void OnSuccessSubmit(int id)
        {
            mediaContents.Remove(mediaContents.First(f => f.ContentId == id));
            notificationUtility.ShowNotification("Success", "Deleted content");
        }

        private void OnFailedSubmit()
        {
            notificationUtility.ShowNotification("Failed", "Failed to delete content");   
        }

        private void OnSearch(string term)
        {
            searchTerm = term ?? "";
        }

        private void ShowCardDetails(int id)
        {
            contentToOpen = id;
            showDetails = true;
        }

        private void CloseDetailView()
        {
            showDetails = false;
        }

        private void ShowCards()
        {
            cardView = true;
        }
        
        private void ShowTable()
        {
            cardView = false;
        }

    }
}