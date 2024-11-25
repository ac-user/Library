using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ViewModels = Library.Models.ViewModels;

namespace Library.Components
{
    public partial class Home
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
            Navigation.NavigateTo("/media/books");
        }

        private void NavigateToMusic()
        {
            Navigation.NavigateTo("/media/music");
        }

        private void NavigateToMovies()
        {
            Navigation.NavigateTo("/media/movies");
        }

        private void NavigateToCollection(int id)
        {
            Navigation.NavigateTo($"/collection/{id}");
        }
    }
}