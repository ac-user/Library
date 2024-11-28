using Microsoft.AspNetCore.Components;
using ViewModels = Library.UI.Model.ViewModels;

namespace Library.UI.Components.Media
{
    public partial class NewMedia
    {
        [Parameter]
        public EventCallback OnClose { get; set; }

        private ViewModels.Media.NewMedia newMedia = new ViewModels.Media.NewMedia();
        private ViewModels.Media.MediaType selectedMediaType = ViewModels.Media.MediaType.Book;
        
        private void OnMediaTypeChange(ChangeEventArgs e)
        {
            selectedMediaType = (ViewModels.Media.MediaType)Enum.Parse(typeof(ViewModels.Media.MediaType), e.Value.ToString());
            ClearInputFields();
        }

        private void ClearInputFields()
        {
            newMedia = new ViewModels.Media.NewMedia { MediaType = selectedMediaType };
        }
    }
}