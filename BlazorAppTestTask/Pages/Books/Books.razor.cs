using BlazorAppTestTask.Data.EditModels;
using BlazorAppTestTask.Data.Services;
using BlazorAppTestTask.PageModels;
using Microsoft.AspNetCore.Components;
using BlazorAppTestTask.Pages.Student.Edit;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace BlazorAppTestTask.Pages.Books
{
    public class BooksViewModel : ComponentBase
    {
        [Inject] protected BooksService Service { get; set; }
        [Inject] protected IJSRuntime jsruntime { get; set; }

        public List<BooksItemViewModel> Model { get; set; } = new List<BooksItemViewModel>();

        public BooksItemViewModel mCurrentItem;
        public EditBooksItemViewModel mEditViewModel = new EditBooksItemViewModel();

        public bool isRemove;
        public int valueFilter = 0;

        protected async override Task OnInitializedAsync()
        {
            StartSearch();
            //Model = Service.GetAll();
            await base.OnInitializedAsync();
        }

        public string filteringName { get; set; }

        public string filteringAuthor { get; set; }

        public string filteringPages { get; set; }

        public string FilteringName
        {
            get { return filteringName; }
            set { filteringName = value; StartSearch(); }
        }

        public string FilteringAuthor
        {
            get { return filteringAuthor; }
            set { filteringAuthor = value; StartSearch(); }
        }

        public string FilteringPages
        {
            get { return filteringPages; }
            set { filteringPages = value; StartSearch(); }
        }

        public void StartSearch()
        {
            Model = Service.GetFiltering(filteringName, filteringAuthor, filteringPages);
        }

        public void CreateItem()
        {
            mCurrentItem = new BooksItemViewModel();
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
        public void ShowDialogDelete(BooksItemViewModel item)
        {
            mCurrentItem = item;
            isRemove = true;
        }
        public void EditItem(BooksItemViewModel item)
        {
            mCurrentItem = item;
            mEditViewModel.Item = item;
            mEditViewModel.IsOpened = true;
        }

        public void Save(BooksItemViewModel item)
        {
            try
            {
                if (item.BookId > 0)
                {
                    var newItem = Service.Update(item);
                    var index = Model.FindIndex(x => x.BookId == newItem.BookId);
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
                }
                return;

            }

        }

        public async Task GenerateExcel(BooksItemViewModel item)
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
            cell.SetCellValue("Название");

            cell = row.CreateCell(2);
            cell.SetCellValue("Дата");

            cell = row.CreateCell(3);
            cell.SetCellValue("Автор");

            cell = row.CreateCell(4);
            cell.SetCellValue("Страницы");

            //Тело таблицы
            foreach (var model in Model)
            {
                row = worksheet.CreateRow(rowNumber++);
                //Id
                cell = row.CreateCell(0);
                cell.SetCellValue(model.BookId);
                //Название
                cell = row.CreateCell(1);
                cell.SetCellValue(model.Name);
                //Дата
                cell = row.CreateCell(2);
                cell.SetCellValue(model.DateOfPublish.ToString());
                //Автор
                cell = row.CreateCell(3);
                cell.SetCellValue(model.Author);
                //Страницы
                cell = row.CreateCell(4);
                cell.SetCellValue(model.Pages);
            }

            MemoryStream ms = new MemoryStream();
            workbook.Write(ms, false);
            byte[] bytes = ms.ToArray();
            ms.Close();

            await SaveAsFileAsync(jsruntime, "Books List.xlsx", bytes, "application/vnd.ms-excel");

        }

        public async Task SaveAsFileAsync(IJSRuntime js, string filename, byte[] data, string type = "application/octet-stream")
        {
            await js.InvokeAsync<object>("saveAsFile", filename, Convert.ToBase64String(data));
        }
    }
}
