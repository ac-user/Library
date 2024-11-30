using AutoMapper;
using Library.UI.Adapters;
using Microsoft.AspNetCore.Components;
using ViewModels = Library.UI.Model.ViewModels;

namespace Library.UI.Components.Media.Book
{
    public partial class BookView : ViewBase
    {
        [Inject]
        protected IBookAdapter BookAdapter { get; set; }

        [Parameter]
        public int Id { get; set; }


        public ViewModels.Media.Book.Book Book;
        private ViewModels.Media.Book.EditableBook editableBook;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                await GetAsync();
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task GetAsync()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Book = Mapper.Map<ViewModels.Media.Book.Book>(await BookAdapter.GetAsync(Utilities.Account.AccountId, Id, cts.Token));
            cts.Dispose();
            StateHasChanged();
        }

        private void EnableEditMode()
        {
            editableBook = Mapper.Map<ViewModels.Media.Book.EditableBook>(Book);
            editMode = true;
        }

    }
}