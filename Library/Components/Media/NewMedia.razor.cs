using AutoMapper;
using Library.Adapters;
using Library.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using ViewModels = Library.Models.ViewModels;
using AdapterModels = Library.Models.Adapter;

namespace Library.Components.Media
{
    public partial class NewMedia
    {
        [Inject] 
        protected IMediaAdpter Adapter {  get; set; }

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
        //private EditContext editContext;

        //protected override void OnInitialized()
        //{
        //    editContext = new EditContext(newBook);
        //}


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
            notificationMessage = $"Successfully added {name}";
            JSRuntime.InvokeVoidAsync("ShowToast");
            OnClose.InvokeAsync();
        }

        private void OnFailedSubmit()
        {
            string name = GetNewMediaName();
            notificationTitle = "Failed New Media";
            notificationMessage = $"Failed to add {name}";
            JSRuntime.InvokeVoidAsync("ShowToast");
        }

        private async Task HandleValidSubmit()
        {
            using var command = new CommandUtility();

            if (selectedMediaType == ViewModels.Media.MediaType.Book)
            {
                var newBook = Mapper.Map<AdapterModels.Media.Book.BookCreationRequest>(newMedia.Book);
                await command.ExecuteAsync((request, token) => Adapter.CreateAsync(1, request, token),
                                            newBook,
                                            OnSuccessSubmit,
                                            OnFailedSubmit);
            }
            else if (selectedMediaType == ViewModels.Media.MediaType.Music)
            {
                var newMusic = Mapper.Map<AdapterModels.Media.Music.MusicCreationRequest>(newMedia.Music);
                await command.ExecuteAsync((request, token) => Adapter.CreateAsync(1, request, token),
                                            newMusic,
                                            OnSuccessSubmit,
                                            OnFailedSubmit);
            }
            else if (selectedMediaType == ViewModels.Media.MediaType.Movie)
            {
                var newMovie = Mapper.Map<AdapterModels.Media.Movies.MovieCreationRequest>(newMedia.Movie);
                await command.ExecuteAsync((request, token) => Adapter.CreateAsync(1, request, token),
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