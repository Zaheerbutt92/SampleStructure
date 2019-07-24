using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        T Find(params object[] keyValues);
        IEnumerable<T> GetAll();
        T Add(T entity);
        void AddRange(List<T> entities);
        void Delete(object id);
        void Delete(T entityToDelete);
        void DeleteAll();
        T Update(T entityToUpdate);
        /// <summary>
        /// Prevent updating the list of columns passed like created_by and created_date
        /// </summary>
        /// <returns></returns>
        T Update(T entityToUpdate, List<string> unChangedColumns);
        IQueryable<T> GetQueryable();
        IQueryable<T> Query(Expression<Func<T, bool>> filter);
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetAllIncluding(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> Filter(Expression<Func<T, bool>> filter, Expression<Func<T, string>> sortExpression, out int total,
            int index = 1, int size = 50, params Expression<Func<T, object>>[] includeProperties);

    }
}
