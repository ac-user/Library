using Library.UI.Adapters;
using Microsoft.AspNetCore.Components;
using ViewModels = Library.UI.Model.ViewModels;

namespace Library.UI.Components.Media.Music
{
    public partial class MusicView : ViewBase
    {
        [Inject]
        protected IMusicAdapter MusicAdapter { get; set; }

        [Parameter]
        public int Id { get; set; }


        public ViewModels.Media.Music.Music Music;
        private ViewModels.Media.Music.EditableMusic editableMusic;
        
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetAsync();
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task GetAsync()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Music = Mapper.Map<ViewModels.Media.Music.Music>(await MusicAdapter.GetAsync(Utilities.Account.AccountId, Id, cts.Token));
            cts.Dispose();
            StateHasChanged();
        }

        private void EnableEditMode()
        {
            editableMusic = Mapper.Map<ViewModels.Media.Music.EditableMusic>(Music);
            editMode = true;
        }
    }
}