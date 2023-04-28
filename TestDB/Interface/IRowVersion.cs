using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDB.Interface
{
    public interface IRowVersion
    {
        public byte[] RowVersion { get; set; }
    }
}
