using Library.UI.Adapters;
using Microsoft.AspNetCore.Components;
using ViewModels = Library.UI.Model.ViewModels;

namespace Library.UI.Components.Media.Movie
{
    public partial class MovieView : ViewBase
    {
        [Inject]
        protected IMovieAdapter MovieAdapter { get; set; }


        [Parameter]
        public int Id { get; set; }


        public ViewModels.Media.Movie.Movies Movie;
        private ViewModels.Media.Movie.EditableMovie editableMovie;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetAsync();
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task GetAsync()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Movie = Mapper.Map<ViewModels.Media.Movie.Movies>(await MovieAdapter.GetAsync(Utilities.Account.AccountId, Id, cts.Token));
            cts.Dispose();
            StateHasChanged();
        }

        private void EnableEditMode()
        {
            editableMovie = Mapper.Map<ViewModels.Media.Movie.EditableMovie>(Movie);
            editMode = true;
        }
    }
}