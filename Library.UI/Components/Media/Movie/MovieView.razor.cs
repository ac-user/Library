using Microsoft.AspNetCore.Components;
using ViewModels = Library.UI.Model.ViewModels;

namespace Library.UI.Components.Media.Movie
{
    public partial class MovieView : ViewBase
    {
        [Parameter]
        public ViewModels.Media.Movie.Movies Movie { get; set; }


        private ViewModels.Media.Movie.EditableMovie editableMovie;

        private void EnableEditMode()
        {
            editableMovie = Mapper.Map<ViewModels.Media.Movie.EditableMovie>(Movie);
            editMode = true;
        }
    }
}