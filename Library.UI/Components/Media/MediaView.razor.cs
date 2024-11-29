using Library.UI.Model.ViewModels.Media;
using Microsoft.AspNetCore.Components;

namespace Library.UI.Components.Media
{
    public partial class MediaView
    {
        [Parameter]
        public MediaType MediaType { get; set; }

        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public EventCallback OnClose { get; set; }
    }
}