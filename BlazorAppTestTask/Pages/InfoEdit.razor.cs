using BlazorAppTestTask.Data.EditModels;
using BlazorAppTestTask.PageModels;
using BlazorAppTestTask.Pages.Student;
using Microsoft.AspNetCore.Components;

namespace BlazorAppTestTask.Pages
{
    public class InfoEditViewModel : StudentViewModel
    {
        [Parameter]
        public EditObshagaItemViewModel ViewModel1 { get; set; }

        [Parameter]
        public bool IsInfo { get; set; }

        [Parameter]
        public EventCallback<ObshagaItemViewModel> Save { get; set; }

        [Parameter]
        public EventCallback<ObshagaItemViewModel> Reload { get; set; }

        public List<StudentItemViewModel> Model { get; set; } = new List<StudentItemViewModel>();
    }
}
