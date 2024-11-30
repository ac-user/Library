using AutoMapper;
using Library.UI.Adapters;
using Microsoft.AspNetCore.Components;
using ViewModels = Library.UI.Model.ViewModels.Media;

namespace Library.UI.Components.Media
{
    public partial class Collection
    {
        [Inject]
        protected ICollectionAdapter CollectionAdapter { get; set; }

        [Inject]
        protected IMapper Mapper { get; set; }

        [Inject]
        protected NavigationManager Navigation { get; set; }


        [Parameter]
        public int collectionId { get; set; }


        private ViewModels.Collection collection;
        private string searchTerm = "";
        private bool showMedia;
        private bool addMedia;
        private int contentToOpen;

        protected override async Task OnParametersSetAsync()
        {
            await GetCollectionAsync();
            await base.OnParametersSetAsync();
        }

        private async Task GetCollectionAsync()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            collection = Mapper.Map<ViewModels.Collection>(await CollectionAdapter.GetAsync(Utilities.Account.AccountId, collectionId, cts.Token));
            cts.Dispose();
        }

        private void OnClickAddCollection()
        {
            Navigation.NavigateTo($"/new-collection");
        }

        private void NavigateToCollection(int id)
        {
            Navigation.NavigateTo($"/collection/{id}");
        }

        private void OnClickMedia(int id)
        {
            contentToOpen = id;
            showMedia = true;
        }

        private void OnClickAddMedia()
        {
            addMedia = true;
        }

        private void CloseMediaPopup()
        {
            showMedia = false;
            addMedia = false;
        }

        private void OnSearch(string searchCriteria)
        {
            searchTerm = searchCriteria ?? "";
        }
    }
}