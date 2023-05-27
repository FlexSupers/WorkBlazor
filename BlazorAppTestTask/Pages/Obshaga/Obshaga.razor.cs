using BlazorAppTestTask.Data.EditModels;
using BlazorAppTestTask.Data.Services;
using BlazorAppTestTask.PageModels;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace BlazorAppTestTask.Pages.Obshaga
{
    public class ObshagaViewModel : ComponentBase
    {
        [Inject] protected ObshagaService Service { get; set; }
        [Inject] protected IJSRuntime jsruntime { get; set; }

        public List<ObshagaItemViewModel> Model { get; set; } = new List<ObshagaItemViewModel>();

        public ObshagaItemViewModel mCurrentItem;
        public EditObshagaItemViewModel mEditViewModel = new EditObshagaItemViewModel();

        public bool isRemove;
        public bool isInfoOpen;
        public int valueFilter = 0;


        protected async override Task OnInitializedAsync()
        {
            StartSearch();
            //Model = Service.GetAll();
            await base.OnInitializedAsync();
        }

        public string filteringName { get; set; }

        public string FilteringName
        {
            get { return filteringName; }
            set { filteringName = value; StartSearch(); }
        }

        public void StartSearch()
        {
            Model = Service.GetFiltering(filteringName);
        }

        public void CreateItem()
        {
            mCurrentItem = new ObshagaItemViewModel();
            mEditViewModel.Item = mCurrentItem;
            mEditViewModel.IsOpened = true;


        }
        public void DeleteItem(bool ans)
        {
            if (ans == true)
            {
                Service.Delete(mCurrentItem);
                Model.Remove(mCurrentItem);
            }
            isRemove = false;
            StateHasChanged();

        }
        
        public void ShowDialogDelete(ObshagaItemViewModel item)
        {
            mCurrentItem = item;
            isRemove = true;
        }

        public void CloseInfo()
        {
            isInfoOpen = false;
        }

        public void ShowDialogInfo(ObshagaItemViewModel item)
        {
            isInfoOpen = true;
        }

        public void EditItem(ObshagaItemViewModel item)
        {
            mCurrentItem = item;
            mEditViewModel.Item = item;
            mEditViewModel.IsOpened = true;
        }

        public void Save(ObshagaItemViewModel item)
        {
            try
            {
                if (item.ObsId > 0)
                {
                    var newItem = Service.Update(item);
                    var index = Model.FindIndex(x => x.ObsId == newItem.ObsId);
                    Model[index] = newItem;
                }
                else
                {
                    var newItem = Service.Create(item);
                    Model.Add(newItem);
                }
                mEditViewModel.IsOpened = false;
                StateHasChanged();

            }
            catch (Exception ex)
            {
                if (ex is DbUpdateConcurrencyException)
                {
                    mEditViewModel.IsConcurency = true;
                    mEditViewModel.ConcurencyErrorText = "Данные не актуальны! Обновите страницу";
                }
                return;

            }

        }

        public void ReloadItem(ObshagaItemViewModel item)
        {
            try
            {
                var reloadItem = Service.ReloadItem(item);
                if (reloadItem == null)
                {
                    Model.Remove(item);
                }
                else
                {
                    if (mEditViewModel.IsConcurency)
                    {
                        mEditViewModel.Item = reloadItem;
                    }

                    var index = Model.FindIndex(x => x.ObsId == item.ObsId);
                    if (reloadItem.Item == null)
                    {
                        mEditViewModel.IsOpened = false;
                        Model.RemoveAt(index);
                    }
                    else
                    {
                        mEditViewModel.IsConcurency = false;
                        Model[index] = reloadItem;
                    }
                }
                StateHasChanged();
            }
            catch (Exception e)
            {
                mCurrentItem = item;
            }
        }

        public async Task GenerateExcel(ObshagaItemViewModel item)
        {
            mCurrentItem = item;

            IWorkbook workbook = new XSSFWorkbook();

            var dataFormat = workbook.CreateDataFormat();
            var dataStyle = workbook.CreateCellStyle();
            dataStyle.DataFormat = dataFormat.GetFormat("dd.MM.yyyy");

            ISheet worksheet = workbook.CreateSheet("Sheet1");

            int rowNumber = 0;
            IRow row = worksheet.CreateRow(rowNumber++);

            //Заголовок таблицы
            ICell cell = row.CreateCell(0);
            cell.SetCellValue("Id");

            cell = row.CreateCell(1);
            cell.SetCellValue("Адрес");

            cell = row.CreateCell(2);
            cell.SetCellValue("Кол-во комнат");

            //Тело таблицы

            foreach (var model in Model)
            {
                row = worksheet.CreateRow(rowNumber++);
                //Id
                cell = row.CreateCell(0);
                cell.SetCellValue(model.ObsId);
                //Адрес
                cell = row.CreateCell(1);
                cell.SetCellValue(model.Address);
                //Кол-во комнат
                cell = row.CreateCell(2);
                cell.SetCellValue((double)model.NumRooms);
            }

            MemoryStream ms = new MemoryStream();
            workbook.Write(ms, false);
            byte[] bytes = ms.ToArray();
            ms.Close();

            await SaveAsFileAsync(jsruntime, "Obshaga List.xlsx", bytes, "application/vnd.ms-excel");

        }

        public async Task SaveAsFileAsync(IJSRuntime js, string filename, byte[] data, string type = "application/octet-stream")
        {
            await js.InvokeAsync<object>("saveAsFile", filename, Convert.ToBase64String(data));
        }
    }
}
