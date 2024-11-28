using Microsoft.AspNetCore.Components;
using ViewModels = Library.UI.Model.ViewModels;

namespace Library.UI.Components.Media.Music
{
    public partial class MusicView : ViewBase
    {
        [Parameter]
        public ViewModels.Media.Music.Music Music { get; set; }


        private ViewModels.Media.Music.EditableMusic editableMusic;

        private void EnableEditMode()
        {
            editableMusic = Mapper.Map<ViewModels.Media.Music.EditableMusic>(Music);
            editMode = true;
        }
    }
}