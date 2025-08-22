using AutoMapper;
using Microsoft.AspNetCore.Components;

namespace Library.UI.Components.Media
{
    public partial class ViewBase : ComponentBase
    {
        [Inject]
        protected IMapper Mapper { get; set; }

        [Parameter]
        public EventCallback OnClose { get; set; }
        

        public bool editMode;

        public void TurnOffEditMode()
        {
            editMode = false;
        }
    }


}
