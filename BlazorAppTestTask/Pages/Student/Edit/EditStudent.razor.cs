using BlazorAppTestTask.Data.EditModels;
using BlazorAppTestTask.PageModels;
using Microsoft.AspNetCore.Components;

namespace BlazorAppTestTask.Pages.Student.Edit
{
    public class EditStudentViewModel : ComponentBase
    {
        [Parameter]
        public EditStudentItemViewModel ViewModel { get; set; }

        [Parameter]
        public EventCallback<StudentItemViewModel> Save { get; set; }

        [Parameter]
        public EventCallback<StudentItemViewModel> Reload { get; set; }
    }
}
