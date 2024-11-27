using AutoMapper;
using Library.UI.Adapters;
using Library.UI.Model.ViewModels.Media;
using Microsoft.AspNetCore.Components;

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
        protected IMapper Mapper { get; set; }

        [Parameter]
        public string mediaType { get; set; }

        private MediaType pageMediaType;
        private List<MediaCollection> mediaContents = new();

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
            StateHasChanged();
        }
    }
}