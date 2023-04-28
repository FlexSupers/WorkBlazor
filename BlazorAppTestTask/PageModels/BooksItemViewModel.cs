using System.ComponentModel.DataAnnotations;
using TestDB.Models;

namespace BlazorAppTestTask.PageModels
{
    public class BooksItemViewModel
    {
        private Books item;
        public Books Item => item;

        public BooksItemViewModel()
        {
            item = new Books();
        }

        public BooksItemViewModel(Books modelBooks)
        {
            item = modelBooks;
        }

        [Key]
        public int BookId
        {
            get => item.BookId;
            set => item.BookId = value;
        }

        [Required]
        public string Name
        {
            get => item.Name;
            set => item.Name = value;
        }

        [Required]
        public DateTime DateOfPublish
        {
            get => item.DateOfPublish;
            set => item.DateOfPublish = value;

        }
        public string Author
        {
            get => item.Author;
            set => item.Author = value;
        }

        [Required]
        public string Pages
        {
            get => item.Pages;
            set => item.Pages = value;
        }
    }
}
