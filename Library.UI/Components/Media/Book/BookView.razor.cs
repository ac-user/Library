using AutoMapper;
using Microsoft.AspNetCore.Components;
using ViewModels = Library.UI.Model.ViewModels;

namespace Library.UI.Components.Media.Book
{
    public partial class BookView : ViewBase
    {
        [Parameter]
        public ViewModels.Media.Book.Book Book { get; set; }


        private ViewModels.Media.Book.EditableBook editableBook;

        private void EnableEditMode()
        {
            editableBook = Mapper.Map<ViewModels.Media.Book.EditableBook>(Book);
            editMode = true;
        }

    }
}