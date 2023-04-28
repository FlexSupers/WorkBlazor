using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HousingDB.Models
{
    [Table("Obshaga")]
    public class Obshaga
    {
        [Key]
        public int ObsId { get; set; }
        public string Address { get; set; }
        public int? NumRooms { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}

