using Library.UI.Adapters;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using AutoMapper;
using Library.UI.Utilities;
using ViewModels = Library.UI.Model.ViewModels;
using AdapterModels = Library.Models;

namespace Library.UI.Components.Media.Music
{
    public partial class MusicEditForm : MediaEditFormBase
    {
        [Inject]
        protected IMusicAdapter MusicAdapter { get; set; }

        [Parameter]
        public ViewModels.Media.Music.EditableMusic EditableMusicModel { get; set; }


        private async Task HandleValidMusicSubmit()
        {
            using var command = new CommandUtility();
            if (IsCreate)
            {
                var newMusic = Mapper.Map<AdapterModels.Media.Music.MusicCreationRequest>(EditableMusicModel);
                await command.ExecuteAsync((request, token) => MusicAdapter.CreateAsync(Utilities.Account.AccountId, request, token),
                                            newMusic,
                                            OnSuccessSubmit,
                                            OnFailedSubmit);
            }
            else
            {
                var modifyMusic = Mapper.Map<AdapterModels.Media.Music.MusicModificationRequest>(EditableMusicModel);
                await command.ExecuteAsync((request, token) => MusicAdapter.ModifyAsync(Utilities.Account.AccountId, request, token),
                                            modifyMusic,
                                            OnSuccessSubmit,
                                            OnFailedSubmit);
            }
        }

        private void OnSuccessSubmit()
        {
            if (IsCreate)
            {
                notificationUtility.ShowNotification("Created New Music", $"Successfully added {EditableMusicModel.Title}");
            }
            else
            {
                notificationUtility.ShowNotification("Modified Music", $"Successfully modified {EditableMusicModel.Title}");
            }
            OnSuccess.InvokeAsync();
        }
        private void OnFailedSubmit()
        {
            if (IsCreate)
            {
                notificationUtility.ShowNotification("Failed New Music", $"Failed to add {EditableMusicModel.Title}");
            }
            else
            {
                notificationUtility.ShowNotification("Failed Modifying Music", $"Failed modifying {EditableMusicModel.Title}");
            }
        }
    }
}