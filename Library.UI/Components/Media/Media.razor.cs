using AutoMapper;
using Library.UI.Adapters;
using Library.UI.Model.ViewModels.Media;
using Library.UI.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

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

        private MediaType pageMediaType;
        private List<MediaCollection> mediaContents = new();
        private NotificationUtility notificationUtility;
        private bool cardView = true;
        private string searchTerm = "";

        protected override void OnParametersSet()
        {
            pageMediaType = (MediaType)Enum.Parse(typeof(MediaType), mediaType);
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

            if (pageMediaType == MediaType.Book)
            {
                mediaContents = Mapper.Map<List<MediaCollection>>(await BookAdapter.GetAsync(1, cts.Token));
            }
            else if (pageMediaType == MediaType.Music)
            {
                mediaContents = Mapper.Map<List<MediaCollection>>(await MusicAdapter.GetAsync(1, cts.Token));
            }
            else if (pageMediaType == MediaType.Movie)
            {
                mediaContents = Mapper.Map<List<MediaCollection>>(await MovieAdapter.GetAsync(1, cts.Token));
            }
            cts.Dispose();
            StateHasChanged();
        }

        private async Task DeleteMediaContentAsync(int id)
        {
            //using var command = new CommandUtility();
            
            //if (pageMediaType == MediaType.Book)
            //{
            //    await command.ExecuteAsync((token) => BookAdapter.DeleteAsync(1, id, token),
            //                            parameter: null,
            //                            OnSuccessSubmit,
            //                            OnFailedSubmit);
            //}
            //else if (pageMediaType == MediaType.Music)
            //{
            //    await command.ExecuteAsync((token) => MusicAdapter.DeleteAsync(1, id, token),
            //                            parameter: null,
            //                            OnSuccessSubmit,
            //                            OnFailedSubmit);
            //}
            //else if (pageMediaType == MediaType.Movie)
            //{
            //    await command.ExecuteAsync((token) => MovieAdapter.DeleteAsync(1, id, token),
            //                            parameter: null,
            //                            OnSuccessSubmit,
            //                            OnFailedSubmit);
            //}
        }
        private void OnSuccessSubmit()
        {
            notificationUtility.ShowNotification("Success", "Failed to delete content");
        }

        private void OnFailedSubmit()
        {
            notificationUtility.ShowNotification("Failed", "Failed to delete content");   
        }

        private void OnSearch(string term)
        {
            if(term == null)
            {
                searchTerm = "";
            }
            else
            {
                searchTerm = term;
            }
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