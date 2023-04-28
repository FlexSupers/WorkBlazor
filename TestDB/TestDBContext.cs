using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDB.Models;

namespace TestDB
{
    public class TestDBContext : DbContext
    {
        public TestDBContext(DbContextOptions<TestDBContext> options) : base(options)
        {
            
        }
        public DbSet<Student> StudentDbSet { get; set; }
        public DbSet<Books> BooksDbSet { get; set; }

        public List<Student> GetFilteringStudent(string name,int bookId)
        {
            var sql = $"SELECT * FROM Student WHERE (FirstName LIKE '%{name}%' OR MiddleName LIKE '%{name}%' OR LastName LIKE '%{name}%')  ";
            
            if (bookId > 0)
            {
                sql += $" AND BookId = {bookId}";
            }
            var result = StudentDbSet.FromSqlRaw(sql).ToList();
            return result;
            
        }
        public List<Books> GetFilteringBooks(string name)
        {
            var sql = $"SELECT * FROM Books WHERE Name LIKE '%{name}%' OR Author LIKE '%{name}%' OR Pages LIKE '%{name}%' ";
            //if (bookId > 0)
            //{
            //    sql = $"AND BookId = {bookId}";
            //}
            var result = BooksDbSet.FromSqlRaw(sql).ToList();
            return result;

        }

    }
}
