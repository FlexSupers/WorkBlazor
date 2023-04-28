using BlazorAppTestTask.Data.EditModels;
using BlazorAppTestTask.PageModels;
using Microsoft.AspNetCore.Components;

namespace BlazorAppTestTask.Pages.Obshaga.Edit
{
    public class EditObshagaViewModel : ComponentBase
    {
        [Parameter]
        public EditObshagaItemViewModel ViewModel { get; set; }

        [Parameter]
        public EventCallback<ObshagaItemViewModel> Save { get; set; }

        [Parameter]
        public EventCallback<ObshagaItemViewModel> Reload { get; set; }
    }
}
