using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Library.UI.Adapters;
using AutoMapper;
using Library.UI.Utilities;
using ViewModels = Library.UI.Model.ViewModels;
using AdapterModels = Library.Models;

namespace Library.UI.Components.Media.Movie
{
    public partial class MovieEditForm : MediaEditFormBase
    {
        [Inject]
        protected IMovieAdapter MovieAdapter { get; set; }

        [Parameter]
        public ViewModels.Media.Movie.EditableMovie EditableMovieModel { get; set; }

        private List<string> Genres = new List<string>()
        {
            "Action", "Adventure", "Comedy", "Drama", "Horror", "Romantic", "Documentary", "Animated"
        };
        private string selectGenre;

        private async Task HandleValidMovieSubmit()
        {
            using var command = new CommandUtility();
            if (IsCreate)
            {
                var newMovie = Mapper.Map<AdapterModels.Media.Movies.MovieCreationRequest>(EditableMovieModel);
                await command.ExecuteAsync((request, token) => MovieAdapter.CreateAsync(Utilities.Account.AccountId, request, token),
                                            newMovie,
                                            onSuccess: (() => OnSuccessSubmit()),
                                            onFailure: OnFailedSubmit);
            }
            else
            {
                var modifyMovie = Mapper.Map<AdapterModels.Media.Movies.MovieModificationRequest>(EditableMovieModel);
                await command.ExecuteAsync((request, token) => MovieAdapter.ModifyAsync(Utilities.Account.AccountId, request, token),
                                            modifyMovie,
                                            onSuccess: (() => OnSuccessSubmit()),
                                            onFailure: OnFailedSubmit);
            }
        }

        private async Task OnSuccessSubmit()
        {
            if(IsCreate)
            {
                notificationUtility.ShowNotification("Created New Movie", $"Successfully added {EditableMovieModel.Title}");
            }
            else
            {
                notificationUtility.ShowNotification("Created New Movie", $"Successfully added {EditableMovieModel.Title}");
            }
            await Task.Delay(2000);
            await OnSuccess.InvokeAsync();
        }
        private void OnFailedSubmit()
        {
            if (IsCreate)
            {
                notificationUtility.ShowNotification("Failed New Movie", $"Failed to add {EditableMovieModel.Title}");
            }
            else
            {
                notificationUtility.ShowNotification("Failed New Movie", $"Failed to add {EditableMovieModel.Title}");
            }
        }
    }
}