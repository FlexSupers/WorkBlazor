using BlazorAppTestTask.PageModels;
using System;
using TestDB;
using TestDB.Models;

namespace BlazorAppTestTask.Data.Services
{
    public class BooksService
    {
        private TestDBContext db;
        private TestRepos<Books> repos;


        public BooksService(TestDBContext context)
        {
            db = context;
            repos = new TestRepos<Books>(context);
        }
        public List<BooksItemViewModel> GetAll()
        {
            var list = repos.Get().ToList();
            var result = list.Select(x => Convert(x)).ToList();
            return result;
        }

        private static BooksItemViewModel Convert(Books model1)
        {
            var item = new BooksItemViewModel(model1);
            return item;
        }

        public BooksItemViewModel Update(BooksItemViewModel modelBooks)
        {
            var x = repos.FindById(modelBooks.BookId);
            x.Name = modelBooks.Name;
            x.DateOfPublish = modelBooks.DateOfPublish;
            x.Author = modelBooks.Author;
            x.Pages = modelBooks.Pages;


            var result = Convert(repos.Update(x));
            return result;
        }

        public BooksItemViewModel Create(BooksItemViewModel modelBooks)
        {
            var result = repos.Create(modelBooks.Item);
            return Convert(result);
        }

        public void Delete(BooksItemViewModel modelBooks)
        {
            var y = repos.FindById(modelBooks.BookId);
            var result = repos.Delete(y);
        }

        public List<BooksItemViewModel> GetFiltering(string name, string author, string pages)
        {
            var list = db.GetFilteringBooks(name);
            var result = list.Select(Convert).ToList();
            return result;
        }
        public string GetName(int? bookId)
        {
            var item = repos.FindById(bookId ?? 0);
            return item.Name;
        }
    }
}
