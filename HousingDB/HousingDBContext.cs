using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HousingDB.Models;

namespace HousingDB
{
    public class HousingDBContext : DbContext
    {
        public HousingDBContext(DbContextOptions<HousingDBContext> options) : base(options)
        {

        }
        public DbSet<Obshaga> ObshagaDbSet { get; set; }

        public List<Obshaga> GetFilteringObshaga(string name)
        {
            var sql = $"SELECT * FROM Obshaga WHERE (Address LIKE '%{name}%' OR NumRooms LIKE '%{name}%')"; //сделать нормальный запрос по фильтрам

            
            var result = ObshagaDbSet.FromSqlRaw(sql).ToList();
            return result;

        }
    }
}
