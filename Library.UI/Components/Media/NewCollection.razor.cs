using Microsoft.AspNetCore.Components;
using ViewModels = Library.UI.Model.ViewModels;

namespace Library.UI.Components.Media
{
    public partial class NewCollection
    {
        [Inject]
        protected NavigationManager Navigation {  get; set; }

        private ViewModels.Media.Collection collection;
        private string searchTerm;

        protected override void OnInitialized()
        {
            collection.Name = "New Collection";
            base.OnInitialized();
        }

        private async Task GetCollectionAsync()
        {

        }

        private void OnSearch(string searchCriteria)
        {
            searchTerm = searchCriteria;
        }

        private async Task OnSaveCollection()
        {

        }
    }
}