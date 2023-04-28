using BlazorAppTestTask.PageModels;

namespace BlazorAppTestTask.Data.EditModels
{
    public class EditBooksItemViewModel
    {
        public bool IsOpened { get; set; }
        public bool IsConcurency { get; set; }
        public BooksItemViewModel Item { get; set; }
    }
}
