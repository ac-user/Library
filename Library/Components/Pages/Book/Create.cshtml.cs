using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Library.Data;
using Library.Models.ViewModels;
using Microsoft.AspNetCore.Components;
using Library.Adapters;
using Library.Utilities;
using Library.Models.Adapter;

namespace Library.Components.Pages.Book
{
    public class CreateModel : PageModel
    {
        [Inject]
        protected IBookAdapter _bookAdapter {  get; set; }
        [Inject]
        protected CommandUtility _commandUtility { get; set; }
        [BindProperty]
        public CreateBook Book { get; set; } = default!;


        async Task OnClickCreateAsync()
        {
            if (!ModelState.IsValid)
            {

            }

            var request = new BookCreationRequest();
            await _commandUtility.ExecuteAsync(_bookAdapter.CreateAsync,
                                               request,
                                               OnCreationSuccess,
                                               OnCreationFail);
            
        }

        void OnCreationSuccess()
        {
            RedirectToPage("./Index");
        }

        void OnCreationFail()
        {

        }
    }
}
