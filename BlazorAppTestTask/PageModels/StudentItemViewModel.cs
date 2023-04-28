using System.ComponentModel.DataAnnotations;
using TestDB.Models;

namespace BlazorAppTestTask.PageModels
{
    public class StudentItemViewModel
    {
        private Student item;
        public Student Item => item;

        public StudentItemViewModel()
        {
            item = new Student();
        }

        public StudentItemViewModel(Student modelStudent)
        {
            item = modelStudent;
        }

        [Key]
        public int StudentId
        {
            get => item.StudentId;
            set => item.StudentId = value;
        }

        [Required(ErrorMessage = "Please enter a FirstName")]
        public string FirstName
        {
            get => item.FirstName;
            set => item.FirstName = value;
        }
        [Required(ErrorMessage = "Please enter a LastName")]
        public string LastName
        {
            get => item.LastName;
            set => item.LastName = value;
        }
        public string MiddleName
        {
            get => item.MiddleName;
            set => item.MiddleName = value;
        }

        //[Required]
        public DateTime? Birth
        {
            get => item.Birth;
            set => item.Birth = value;
        }
        public string Location
        {
            get => item.Location;
            set => item.Location = value;
        }

        //[EmailAddress]
        public string Email
        {
            get => item.Email;
            set => item.Email = value;
        }
        public int? BookId
        {
            get => item.BookId;
            set => item.BookId = value;
        }
    }
}
