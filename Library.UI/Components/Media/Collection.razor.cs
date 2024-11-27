using Microsoft.AspNetCore.Components;
using ViewModels = Library.UI.Model.ViewModels;

namespace Library.UI.Components.Media
{
    public partial class Collection
    {
        [Parameter]
        public int CollectionId { get; set; }

        private ViewModels.Media.Collection collection;
        private string searchTerm;
        private bool addMedia;

        protected override async Task OnParametersSetAsync()
        {
            await GetCollectionAsync();
            await base.OnParametersSetAsync();
        }

        private async Task GetCollectionAsync()
        {

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

        private void OnSearch(string searchCriteria)
        {
            searchTerm = searchCriteria;
        }
    }
}