using AutoMapper;
using Library.Models.Media;
using Library.UI.Adapters;
using Library.UI.Model.ViewModels.Media;
using Library.UI.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ViewModels = Library.UI.Model.ViewModels;

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
        protected IMapper Mapper { get; set; }

        [Inject]
        protected NavigationManager Navigation {  get; set; }

        private ViewModels.Media.NewCollection newCollection = new();
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
                await GetAllContentAsync();
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task GetAllContentAsync()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            newCollection.Books = Mapper.Map<List<NewCollectionContent>>(await BookAdapter.GetAsync(1, cts.Token));
            newCollection.Music = Mapper.Map<List<NewCollectionContent>>(await MusicAdapter.GetAsync(1, cts.Token));
            newCollection.Movies = Mapper.Map<List<NewCollectionContent>>(await MovieAdapter.GetAsync(1, cts.Token));
            newCollection.Collections = new();
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

        }
    }
}