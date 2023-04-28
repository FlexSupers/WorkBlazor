using BlazorAppTestTask.Data.EditModels;
using BlazorAppTestTask.PageModels;
using Microsoft.AspNetCore.Components;

namespace BlazorAppTestTask.Pages.Student.Edit
{
    public class DeleteStudentViewModel : ComponentBase
    {
        [Parameter]
        public bool IsOpened { get; set; }

        [Parameter]
        public EventCallback<bool> Answer { get; set; }
    }
}
