using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected IDbContext _dbContext;
        protected IDbSet<T> _dbSet;

        public GenericRepository(IDbContext context)
        {
            this._dbContext = context;
            this._dbSet = context.Set<T>();
        }
        public virtual T Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public virtual IQueryable<T> GetQueryable()
        {
            return _dbSet;
        }

        public virtual IQueryable<T> Query(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Where(filter);
        }

        public virtual T Add(T entity)
        {
            return _dbSet.Add(entity);
        }

        public void AddRange(List<T> entities)
        {
            foreach (var entity in entities)
                _dbSet.Add(entity);
        }
        /// <summary>
        /// Map the values from view model to the main object (used to keep the original values that are not exposed in the View Model object like created by/date)
        /// </summary>
        /// <param name="entityToUpdate">The main entity object</param>
        /// <param name="entityVM">The view model of the passed object</param>
        /// <returns></returns>
        public virtual T Update(T entityToUpdate, object entityVM)
        {
            _dbContext.Entry(entityToUpdate).CurrentValues.SetValues(entityVM);
            return Update(entityToUpdate);
        }

        public virtual T Update(T entityToUpdate)
        {
            return Update(entityToUpdate, new List<string>());
        }

        public virtual T Update(T entityToUpdate, List<string> unChangedColumns)
        {
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
            if (unChangedColumns != null && unChangedColumns.Count > 0)
                foreach (string ColName in unChangedColumns)
                    _dbContext.Entry(entityToUpdate).Property(ColName).IsModified = false;

            return entityToUpdate;
        }


        public virtual void Delete(object id)
        {
            T entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void DeleteAll()
        {
            _dbContext.Database.ExecuteSqlCommand(string.Format("DELETE FROM {0}", typeof(T).Name));
        }

        public virtual IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = GetQueryable();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<T, object>(includeProperty);
            }
            return queryable;
        }

        public IQueryable<T> GetAllIncluding(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = GetQueryable();
            if (includeProperties == null || !includeProperties.Any()) return queryable.Where(predicate);
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
                queryable = queryable.Include<T, object>(includeProperty);
            return queryable.Where(predicate);
        }

        public virtual IQueryable<T> Filter(Expression<Func<T, bool>> filter, Expression<Func<T, string>> sortExpression, out int total, int index = 1, int size = 50, params Expression<Func<T, object>>[] includeProperties)
        {
            var skipCount = index * size - size;
            IQueryable<T> queryable = GetQueryable();

            if (includeProperties != null && includeProperties.Any())
            {
                var query = queryable.Include(includeProperties.First());

                foreach (var include in includeProperties.Skip(1))
                {
                    query = query.Include(include);
                }
                queryable = filter != null ? query.Where(filter).OrderBy(sortExpression).AsQueryable() : query.AsQueryable();
            }
            else
            {
                queryable = filter != null ? queryable.Where(filter).OrderBy(sortExpression).AsQueryable() : queryable.AsQueryable();
            }
            queryable = skipCount == 0 ? queryable.Take(size) : queryable.Skip(skipCount).Take(size);
            total = Count(filter);
            return queryable;
        }

        public int Count<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            IQueryable<T> queryable = (IQueryable<T>)GetQueryable();
            return predicate != null ? queryable.Count(predicate) : queryable.Count();
        }

    }
}
