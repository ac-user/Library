using AutoMapper;
using Library.UI.Adapters;
using Library.UI.Model.ViewModels.Media;
using Microsoft.AspNetCore.Components;
using ViewModels = Library.UI.Model.ViewModels;

namespace Library.UI.Components
{
    public partial class Home : ComponentBase
    {
        [Inject]
        protected ICollectionAdapter CollectionAdapter { get; set; }

        [Inject]
        protected IMapper Mapper { get; set; }

        [Inject]
        protected NavigationManager Navigation { get; set; }

        private ViewModels.Home _model = new();
        private bool addMedia;
        private string searchTerm = "";

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Utilities.Account.UserName = "Isabella218";
                Utilities.Account.AccountId = 1;
                await GetCollections();
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task GetCollections()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            _model.Collections = Mapper.Map<List<ViewModels.CollectionCards>>(await CollectionAdapter.GetAsync(Utilities.Account.AccountId, cts.Token));
            cts.Dispose();
            StateHasChanged();
        }

        private void OnClickAddCollection()
        {
            Navigation.NavigateTo($"/new-collection");
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
            searchTerm = term ?? "";
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