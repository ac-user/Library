using Library.UI.Model.ViewModels.Media;
using Microsoft.AspNetCore.Components;

namespace Library.UI.Components.Media
{
    public partial class MediaView<TModel>
    {
        [Parameter]
        public MediaType MediaType { get; set; }

        [Parameter]
        public TModel Value { get; set; }

        [Parameter]
        public EventCallback OnClose { get; set; }
    }
}