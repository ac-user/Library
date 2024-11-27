using Library.UI.Model.ViewModels.Media;
using Microsoft.AspNetCore.Components;
using ViewModels = Library.UI.Model.ViewModels;

namespace Library.UI.Components
{
    public partial class Home : ComponentBase
    {
        [Inject]
        protected NavigationManager Navigation { get; set; }

        private ViewModels.Home _model = new();
        private List<ViewModels.CollectionCards> filteredCollections;
        private bool addMedia;
        private string searchTerm;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetCollections();
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task GetCollections()
        {
            _model.Collections = new();
            StateHasChanged();
        }

        private void OnClickAddCollection()
        {

        }

        private void OnClickAddMedia()
        {
            addMedia = true;
        }

        private void CloseAddMediaPopup()
        {
            addMedia = false;
        }

        private void OnSearch(string term)
        {
            searchTerm = term;
        }

        private void NavigateToBooks()
        {
            Navigation.NavigateTo($"/media/{MediaType.Book.ToString()}");
        }

        private void NavigateToMusic()
        {
            Navigation.NavigateTo($"/media/{MediaType.Music.ToString()}");
        }

        private void NavigateToMovies()
        {
            Navigation.NavigateTo($"/media/{MediaType.Movie.ToString()}");
        }

        private void NavigateToCollection(int id)
        {
            Navigation.NavigateTo($"/collection/{id}");
        }
    }
}