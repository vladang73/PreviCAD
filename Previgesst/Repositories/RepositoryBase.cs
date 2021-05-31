using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Previgesst.Repositories
{
    public class RepositoryBase<T> where T : class
    {
        protected DbContext context;

        protected DbSet<T> dbSet { get; set; }

        public RepositoryBase(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public virtual List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual T Get(int id)
        {
            return dbSet.Find(id);
        }
        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            context.Entry<T>(entity).State = EntityState.Modified;
        }

        public virtual void Delete(int id)
        {
            dbSet.Remove(dbSet.Find(id));
        }

        public virtual int SaveChanges()
        {
            return context.SaveChanges();
        }

        public virtual IQueryable<T> AsQueryable()
        {
            return dbSet.AsQueryable();
        }
    }
}