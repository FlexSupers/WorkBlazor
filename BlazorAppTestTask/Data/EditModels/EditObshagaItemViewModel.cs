using BlazorAppTestTask.PageModels;

namespace BlazorAppTestTask.Data.EditModels
{
    public class EditObshagaItemViewModel
    {
        public bool IsOpened { get; set; }
        public bool IsConcurency { get; set; }
        public ObshagaItemViewModel Item { get; set; }
        public string ConcurencyErrorText { get; set; }
    }
}
