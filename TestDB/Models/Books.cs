using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestDB.Interface;

namespace TestDB.Models
{
    [Table("Books")]
    public class Books : IRowVersion
    {
        [Key]
        public int BookId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfPublish { get; set; }
        public string Author { get; set; }
        public string Pages { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }




    }
}
