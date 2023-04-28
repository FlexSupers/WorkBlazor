using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDB.Interface;

namespace TestDB.Models
{
   [Table("Student")]
   public class Student : IRowVersion
   {
        [Key]
        public int StudentId { get; set; }
        public string FirstName { get; set;}
        public string MiddleName { get; set; }
        public string LastName { get; set;}
        public DateTime? Birth { get; set; }
        public string Location { get; set; }
        public string Email { get; set;}
        public int? BookId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }




   }
}
