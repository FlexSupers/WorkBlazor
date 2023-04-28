using BlazorAppTestTask.Data.EditModels;
using BlazorAppTestTask.PageModels;
using Microsoft.AspNetCore.Components;

namespace BlazorAppTestTask.Pages.Books.Edit
{
    public class EditBooksViewModel : ComponentBase
    {
        [Parameter]
        public EditBooksItemViewModel ViewModel { get; set; }

        [Parameter]
        public EventCallback<BooksItemViewModel> Save { get; set; }
    }
}
