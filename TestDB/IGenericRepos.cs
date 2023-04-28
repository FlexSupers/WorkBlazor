using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDB
{
    public interface IGenericRepos<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get();// получаем таблицу
        TEntity Create(TEntity item);// создаем элемент(столбец)
        TEntity Update(TEntity item);// редактируем элемент(столбец)
        TEntity Delete(TEntity item);
        TEntity FindById(int id);
    }
}
