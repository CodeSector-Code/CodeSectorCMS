using System;
using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CodeSectorCMS.Domain.Repositories
{
    public class GenericRepository<TEntity> where TEntity : BaseEntity
    {
        internal CmsContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(CmsContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList<TEntity>().Where(a => a.Deleted == false);
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null, // caller wil provide lambda expression based on entity type
            //and expression will return bool value, ex. client => client.Name == "Bojana"
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, // caller will also provide lambda expression
            //but this time input is IQueryable object for the IEntity type
            //ex. q => q.OrderBy (s => s.Name)
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) //eager loading expression
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList().Where(a => a.Deleted == false);
            }

            else
            {
                return query.ToList().Where(a => a.Deleted == false);
            }

        }

        public virtual TEntity GetByID(object id)
        {
            TEntity result = dbSet.Find(id);
            if (result.Deleted)
                return null;
            else
                return result;
        }

        public virtual void Insert(TEntity entity)
        {
            entity.DateCreated = DateTime.Now.ToShortDateString();
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            entityToDelete.Deleted = true;
            Update(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

    }

}

