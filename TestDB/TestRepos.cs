using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDB.Interface;
using TestDB.Models;

namespace TestDB
{
    public class TestRepos<TEntity> : IGenericRepos<TEntity> where TEntity : class
    {
        private TestDBContext context;
        private DbSet<TEntity> dbSet;


        public TestRepos(TestDBContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();

        }

        public TEntity Create(TEntity item)
        {
            var newItem = dbSet.Add(item).Entity;
            context.SaveChanges();
            context.Entry(item).State = EntityState.Detached;
            context.SaveChanges();
            return newItem;
        }

        public TEntity Delete(TEntity item)
        {
            if (item != null)
            {
                dbSet.Attach(item);
                context.Remove(item);
                context.SaveChanges();
            }
            return item;
        }

        public TEntity Update(TEntity item)
        {
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
            context.Entry(item).State = EntityState.Detached;
            context.SaveChanges();
            return item;
        }
        public TEntity Update(TEntity item, byte[] rowversion, string operation = "")
        {
            try
            {
                if (item is IRowVersion)
                {
                    context.Entry(item).OriginalValues["RowVersion"] = rowversion;
                }
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
                context.Entry(item).State = EntityState.Detached;
                context.SaveChanges();

                return item;
            }
            catch(Exception ex)
            {
                context.Entry(item).State = EntityState.Detached;
                throw ex;
            }
        }
        public TEntity FindById(int id)
        {
            var entity = dbSet.Find(id)
;
            if (entity == null)
            {
                return null;
            }
            context.Entry(entity).State = EntityState.Detached;
            context.SaveChanges();
            return entity;
        }


        public IEnumerable<TEntity> Get()
        {
            return dbSet.AsNoTracking().AsQueryable();
        }
        public TEntity Reload(int id)
        {
            var item = dbSet.Find(id);

            if (item == null)
            {
                return null;
            }
            context.Entry(item).State = EntityState.Detached;
            var result = context.Entry(item).GetDatabaseValues();

            return (TEntity)result?.ToObject();
        }

    }
}
