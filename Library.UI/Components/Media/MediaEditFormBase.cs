using AutoMapper;
using Library.UI.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Library.UI.Components.Media
{
    public partial class MediaEditFormBase : ComponentBase
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected IMapper Mapper { get; set; }


        [Parameter]
        public bool IsCreate { get; set; }

        [Parameter]
        public EventCallback OnClose { get; set; }

        [Parameter]
        public EventCallback OnSuccess { get; set; }


        public NotificationUtility notificationUtility;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                notificationUtility = new NotificationUtility(JSRuntime);
            }
            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
