using AutoMapper;
using Library.UI.Adapters;
using Library.UI.Model.ViewModels.Media;
using Microsoft.AspNetCore.Components;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System;
using ViewModels = Library.UI.Model.ViewModels;
using Library.UI.Utilities;
using Library.Models.Media;
using Microsoft.JSInterop;

namespace Library.UI.Components
{
    public partial class Home : ComponentBase
    {
        [Inject]
        protected ICollectionAdapter CollectionAdapter { get; set; }

        [Inject]
        protected IMapper Mapper { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager Navigation { get; set; }

        private ViewModels.Home _model = new();
        private NotificationUtility notificationUtility;
        private bool addMedia;
        private string searchTerm = "";

        protected override void OnInitialized()
        {
            Utilities.Account.UserName = "Isabella218";
            Utilities.Account.AccountId = 1;
            base.OnInitialized();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                notificationUtility = new NotificationUtility(JSRuntime);
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

        private async Task OnDeleteCollectionAsync(int id)
        {
            using var command = new CommandUtility();
            var tuple = (Utilities.Account.AccountId, id);
            await command.ExecuteAsync((tuple, token) => CollectionAdapter.DeleteAsync(Utilities.Account.AccountId, id, new CancellationToken()),
                                        tuple,
                                        onSuccess: (() => OnSuccessSubmit(id)),
                                        onFailure: OnFailedSubmit);
        }

        private void OnSuccessSubmit(int id)
        {
            _model.Collections.Remove(_model.Collections.First(f => f.Id == id));
            notificationUtility.ShowNotification("Success", "Deleted collection");
        }

        private void OnFailedSubmit()
        {
            notificationUtility.ShowNotification("Failed", "Failed to delete collection");
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