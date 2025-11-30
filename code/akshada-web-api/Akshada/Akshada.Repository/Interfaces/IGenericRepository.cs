using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T GetByGuid(string rowId);
        IQueryable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Remove(T entity);

        void Update(T entity);

        T FindFirst(Expression<Func<T, bool>> expression, string includeProperties = "");

        IQueryable<T> GetAllWithInclude(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = "");

        bool Any(Expression<Func<T, bool>> expression);

        bool RemoveIncludingChildren(string includes, Expression<Func<T, bool>> predicate);

        IQueryable<T> FromSqlRawAsync(string sql, params object[] parameters);

        void Remove(IEnumerable<T> entities);
    }
}
