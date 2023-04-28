using BlazorAppTestTask.PageModels;

namespace BlazorAppTestTask.Data.EditModels
{
    public class EditStudentItemViewModel
    {
        public bool IsOpened { get; set; }
        public bool IsConcurency { get; set; }
        public StudentItemViewModel Item { get; set; }
        public List<BooksItemViewModel> Books { get; set; }
        public string ConcurencyErrorText { get; set; }
    }
}
