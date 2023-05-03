using BlazorAppTestTask.Data.EditModels;
using BlazorAppTestTask.Data.Services;
using BlazorAppTestTask.PageModels;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using NPOI.HSSF.Record;
using NPOI.OpenXmlFormats.Dml.Diagram;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace BlazorAppTestTask.Pages.Student
{
    public class StudentViewModel : ComponentBase
    {

        [Inject] protected StudentService Service { get; set; }
        [Inject] protected BooksService BooksService { get; set; }
        [Inject] protected IJSRuntime jsruntime { get; set; }

        public List<StudentItemViewModel> Model { get; set; } = new List<StudentItemViewModel>();

        public List<BooksItemViewModel> ModelBooks { get; set; } = new List<BooksItemViewModel>();

        public StudentItemViewModel mCurrentItem;
        public EditStudentItemViewModel mEditViewModel = new EditStudentItemViewModel();
        public bool isRemove;
        public int valueFilter = 0;
        protected async override Task OnInitializedAsync()
        {
            StartSearch();
            //Model = Service.GetAll();
            await base.OnInitializedAsync();
        }

        public string filteringName { get; set; }

        public string filteringmiddleName { get; set; }

        public string filteringlastName { get; set; }

        public int filteringbookId { get; set; }

        public string FilteringName
        {
            get { return filteringName; }
            set { filteringName = value; StartSearch(); }
        }

        public string FilteringMiddleName
        {
            get { return filteringmiddleName; }
            set { filteringmiddleName = value; StartSearch(); }
        }

        public string FilteringLastName
        {
            get { return filteringlastName; }
            set { filteringlastName = value; StartSearch(); }
        }

        public int FilteringBookId
        {
            get { return filteringbookId; }
            set { filteringbookId = value; StartSearch(); }
        }

        public void StartSearch()
        {
            Model = Service.GetFiltering(filteringName, filteringmiddleName, filteringlastName, filteringbookId);
            ModelBooks = BooksService.GetAll();
        }

        public void CreateItem()
        {
            mCurrentItem = new StudentItemViewModel();
            mEditViewModel.Books = BooksService.GetAll();
            mEditViewModel.Item = mCurrentItem;
            mEditViewModel.IsOpened = true;


        }
        public void DeleteItem(bool ans)
        {
            if(ans == true)
            {
                Service.Delete(mCurrentItem);
                Model.Remove(mCurrentItem);
            }
            isRemove = false;
            StateHasChanged();

        }
        public string GetBookName(int? bookId)
        {
            return BooksService.GetName(bookId);
        }
        public void ShowDialogDelete(StudentItemViewModel item)
        {
            mCurrentItem = item;
            isRemove = true;
        }

        public void EditItem(StudentItemViewModel item)
        {
            mEditViewModel.Books = BooksService.GetAll();
            mCurrentItem = item;
            mEditViewModel.Item = item;
            mEditViewModel.IsOpened = true;
        }

        public void Save(StudentItemViewModel item)
        {
            try
            {
                if (item.StudentId > 0)
                {
                    var newItem = Service.Update(item);
                    var index = Model.FindIndex(x => x.StudentId == newItem.StudentId);
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
                if(ex is DbUpdateConcurrencyException)
                {
                    mEditViewModel.IsConcurency = true;
                    mEditViewModel.ConcurencyErrorText = "Данные не актуальны! Обновите страницу";
                }
                return;

            }

        }

        public void ReloadItem(StudentItemViewModel item)
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

                    var index = Model.FindIndex(x => x.StudentId == item.StudentId);
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

        public async Task GenerateExcel(StudentItemViewModel item)
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
            cell.SetCellValue("Имя");

            cell = row.CreateCell(2);
            cell.SetCellValue("Отчество");

            cell = row.CreateCell(3);
            cell.SetCellValue("Фамилия");

            cell = row.CreateCell(4);
            cell.SetCellValue("Дата рождения");

            cell = row.CreateCell(5);
            cell.SetCellValue("Местоположение");

            cell = row.CreateCell(6);
            cell.SetCellValue("Эл.адрес");

            cell = row.CreateCell(7);
            cell.SetCellValue("Книга");

            //Тело таблицы

            foreach (var model in Model)
            {
                row = worksheet.CreateRow(rowNumber++);
                //Id
                cell = row.CreateCell(0);
                cell.SetCellValue(model.StudentId);
                //Имя
                cell = row.CreateCell(1);
                cell.SetCellValue(model.FirstName);
                //Отчество
                cell = row.CreateCell(2);
                cell.SetCellValue(model.MiddleName);
                //Фамилия
                cell = row.CreateCell(3);
                cell.SetCellValue(model.LastName);
                //Дата Рождения
                cell = row.CreateCell(4);
                cell.SetCellValue(model.Birth.ToString());
                //Локация
                cell = row.CreateCell(5);
                cell.SetCellValue(model.Location);
                //Эл.адрес
                cell = row.CreateCell(6);
                cell.SetCellValue(model.Email);
                //Название книги
                foreach(var booksItem in ModelBooks)
                {
                    cell = row.CreateCell(7);
                    cell.SetCellValue(booksItem.Name);
                }
            }

            MemoryStream ms = new MemoryStream();
            workbook.Write(ms, false);
            byte[] bytes = ms.ToArray();
            ms.Close();

            await SaveAsFileAsync(jsruntime, "Student List.xlsx", bytes, "application/vnd.ms-excel");

        }

        public async Task SaveAsFileAsync(IJSRuntime js, string filename, byte[] data, string type = "application/octet-stream")
        {
            await js.InvokeAsync<object>("saveAsFile", filename, Convert.ToBase64String(data));
        }
    }
}
