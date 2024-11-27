using AutoMapper;
using Library.UI.Adapters;
using Library.UI.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using ViewModels = Library.UI.Model.ViewModels;
using AdapterModels = Library.Models;

namespace Library.UI.Components.Media
{
    public partial class NewMedia
    {
        [Inject] 
        protected IBookAdapter BookAdapter {  get; set; }

        [Inject] 
        protected IMusicAdapter MusicAdapter {  get; set; }
        
        [Inject] 
        protected IMovieAdapter MovieAdapter {  get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected IMapper Mapper {  get; set; }

        [Parameter]
        public EventCallback OnClose { get; set; }

        private ViewModels.Media.NewMedia newMedia = new ViewModels.Media.NewMedia();
        private ViewModels.Media.MediaType selectedMediaType = ViewModels.Media.MediaType.Book;
        private List<string> validationMessages = new List<string>();
        private bool showPopUp = false;
        private string notificationTitle;
        private string notificationMessage;

        private void OnMediaTypeChange(ChangeEventArgs e)
        {
            selectedMediaType = (ViewModels.Media.MediaType)Enum.Parse(typeof(ViewModels.Media.MediaType), e.Value.ToString());
            ClearInputFields();
        }

        private void ClearInputFields()
        {
            newMedia = new ViewModels.Media.NewMedia { MediaType = selectedMediaType };
        }
                
        private string GetNewMediaName()
        {
            string name = "";
            if (!String.IsNullOrWhiteSpace(newMedia.Book.Title))
            {
                name = newMedia.Book.Title;
            }
            else if (!String.IsNullOrWhiteSpace(newMedia.Music.Title))
            {
                name = newMedia.Music.Title;
            }
            else if (!String.IsNullOrWhiteSpace(newMedia.Movie.Title))
            {
                name = newMedia.Movie.Title;
            }

            return name;
        }

        private void OnSuccessSubmit()
        {
            string name = GetNewMediaName();

            notificationTitle = "Created New Media";
            notificationMessage = $"Successfully added {selectedMediaType.ToString()}: {name}";
            JSRuntime.InvokeVoidAsync("showToast");
            OnClose.InvokeAsync();
        }

        private void OnFailedSubmit()
        {
            string name = GetNewMediaName();
            notificationTitle = "Failed New Media";
            notificationMessage = $"Failed to add new {selectedMediaType.ToString()}: {name}";
            JSRuntime.InvokeVoidAsync("showToast");
        }

        private async Task HandleValidBookSubmit()
        {
            if (selectedMediaType == ViewModels.Media.MediaType.Book)
            {
                using var command = new CommandUtility();
                var newBook = Mapper.Map<AdapterModels.Media.Book.BookCreationRequest>(newMedia.Book);
                await command.ExecuteAsync((request, token) => BookAdapter.CreateAsync(1, request, token),
                                            newBook,
                                            OnSuccessSubmit,
                                            OnFailedSubmit);
            }
        }

        private async Task HandleValidMusicSubmit()
        {
            if (selectedMediaType == ViewModels.Media.MediaType.Music)
            {
                using var command = new CommandUtility();
                var newMusic = Mapper.Map<AdapterModels.Media.Music.MusicCreationRequest>(newMedia.Music);
                await command.ExecuteAsync((request, token) => MusicAdapter.CreateAsync(1, request, token),
                                            newMusic,
                                            OnSuccessSubmit,
                                            OnFailedSubmit);
            }
        }
        
        private async Task HandleValidMovieSubmit()
        {
            if (selectedMediaType == ViewModels.Media.MediaType.Movie)
            {
                using var command = new CommandUtility();
                var newMovie = Mapper.Map<AdapterModels.Media.Movies.MovieCreationRequest>(newMedia.Movie);
                await command.ExecuteAsync((request, token) => MovieAdapter.CreateAsync(1, request, token),
                                            newMovie,
                                            OnSuccessSubmit,
                                            OnFailedSubmit);
            }
        }

        private void HandleInvalidSubmit()
        {
            validationMessages.Clear();
            //validationMessages.AddRange(editContext.GetValidationMessages());
        }
    }
}