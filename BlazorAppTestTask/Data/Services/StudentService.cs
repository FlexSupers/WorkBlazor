using BlazorAppTestTask.PageModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TestDB;
using TestDB.Models;

namespace BlazorAppTestTask.Data.Services
{
    public class StudentService
    {
        private TestDBContext db;
        private TestRepos<Student> repos;


        public StudentService(TestDBContext context)
        {
            db = context;
            repos = new TestRepos<Student>(context);
        }
        public List<StudentItemViewModel> GetAll()
        {
            var list = repos.Get().ToList();
            var result = list.Select(x => Convert(x)).ToList();
            return result;
        }

        private static StudentItemViewModel Convert(Student model)
        {
            var item = new StudentItemViewModel(model);
            return item;
        }

        public StudentItemViewModel Update(StudentItemViewModel modelStudent)
        {
            var x = repos.FindById(modelStudent.StudentId);
            x.FirstName = modelStudent.FirstName;
            x.LastName = modelStudent.LastName;
            x.MiddleName = modelStudent.MiddleName;
            x.Location = modelStudent.Location;
            x.Email = modelStudent.Email;
            x.Birth = x.Birth;

            var result = Convert(repos.Update(x,modelStudent.Item.RowVersion));
            return result;
        }

        public StudentItemViewModel Create(StudentItemViewModel modelStudent)
        {
            var result = repos.Create(modelStudent.Item);
            return Convert(result);
        }
        public void Delete(StudentItemViewModel modelStudent)
        {
            var y = repos.FindById(modelStudent.StudentId);
            var result = repos.Delete(y);
        }
        public List<StudentItemViewModel> GetFiltering(string name, string middlename,string lastname, int bookId)
        {
            var list = db.GetFilteringStudent(name, bookId);
            var result = list.Select(Convert).ToList();
            return result;
        }
        public StudentItemViewModel ReloadItem(StudentItemViewModel item)
        {
            var x = repos.Reload(item.StudentId);
            if (x == null)
            {
                return null;
            }
            return Convert(x);
        }
    }
}
