using Microsoft.AspNetCore.Components;
using Library.UI.Adapters;
using Library.UI.Utilities;
using ViewModels = Library.UI.Model.ViewModels;
using AdapterModels = Library.Models;

namespace Library.UI.Components.Media.Book
{
    public partial class BookEditForm : MediaEditFormBase
    {
        [Inject]
        protected IBookAdapter BookAdapter { get; set; }

        [Parameter]
        public ViewModels.Media.Book.EditableBook EditableBookModel { get; set; }

        private List<string> Genres = new List<string>() 
        {
            "Science Fiction", "Fantasy", "Romance", "Mystery", "Thriller", "Non-Fiction", "Historical Fiction", "Biography"
        };

        private async Task HandleValidBookSubmit()
        {
            using var command = new CommandUtility();
            if (IsCreate)
            {
                var newBook = Mapper.Map<AdapterModels.Media.Book.BookCreationRequest>(EditableBookModel);
                await command.ExecuteAsync((request, token) => BookAdapter.CreateAsync(Utilities.Account.AccountId, request, token),
                                            newBook,
                                            onSuccess: (() => OnSuccessSubmit()),
                                            onFailure: OnFailedSubmit);
            }
            else
            {
                var modifyBook = Mapper.Map<AdapterModels.Media.Book.BookModificationRequest>(EditableBookModel);
                await command.ExecuteAsync((request, token) => BookAdapter.ModifyAsync(Utilities.Account.AccountId, request, token),
                                            modifyBook,
                                            onSuccess: (() => OnSuccessSubmit()),
                                            onFailure: OnFailedSubmit);
            }
        }

        private async Task OnSuccessSubmit()
        {
            if (IsCreate)
            {
                notificationUtility.ShowNotification("Created New Book", $"Successfully added {EditableBookModel.Title}");
            }
            else
            {
                notificationUtility.ShowNotification("Modified Book", $"Successfully modified {EditableBookModel.Title}");
            }
            await Task.Delay(2000);
            await OnSuccess.InvokeAsync();
        }

        private void OnFailedSubmit()
        {
            if (IsCreate)
            {
                notificationUtility.ShowNotification("Failed New Book", $"Failed to add {EditableBookModel.Title}");
            }
            else
            {
                notificationUtility.ShowNotification("Failed Modifying Book", $"Failed to modify {EditableBookModel.Title}");
            }
        }
    }
}