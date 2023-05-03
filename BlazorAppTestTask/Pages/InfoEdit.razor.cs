using BlazorAppTestTask.Data.EditModels;
using BlazorAppTestTask.Data.Services;
using BlazorAppTestTask.PageModels;
using BlazorAppTestTask.Pages.Student;
using Microsoft.AspNetCore.Components;
using System.Configuration;

namespace BlazorAppTestTask.Pages
{
    public class InfoEditViewModel : ComponentBase
    {
        [Parameter]
        public bool IsInfoOpen { get; set; }

        [Parameter]
        public EventCallback Close { get; set; }

        public List<StudentItemViewModel> Model { get; set; } = new List<StudentItemViewModel>();
        public List<StudentItemViewModel> AddModel { get; set; } = new List<StudentItemViewModel>();

        [Inject] protected StudentService Service { get; set; }

        public bool Flag { get; set; }

        protected async override Task OnParametersSetAsync()
        {
            Model = Service.GetAll();
            await base.OnParametersSetAsync();
        }

        public void AddItem(StudentItemViewModel item)
        {
            AddModel.Add(item);
            Model.Remove(item);
            StateHasChanged();
        }

        public void DeleteItem(StudentItemViewModel item)
        {
            Model.Add(item);
            AddModel.Remove(item);
            StateHasChanged();
        }
    }
}
