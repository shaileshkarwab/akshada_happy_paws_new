using Akshada.EFCore.DbModels;
using Akshada.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AkshadaPawsContext akshadaPawsContext;
        protected readonly IConfiguration configuration;
        protected readonly IServiceProvider services;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(AkshadaPawsContext akshadaPawsContext, IConfiguration configuration, IServiceProvider services)
        {
            this.akshadaPawsContext = akshadaPawsContext;
            this.configuration = configuration;
            this.services = services;
            _dbSet = akshadaPawsContext.Set<T>();
        }


        public void Add(T entity)
        {
            this.akshadaPawsContext.Set<T>().Add(entity);
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return this.akshadaPawsContext.Set<T>().Where(expression);
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return this.akshadaPawsContext.Set<T>().Any(expression);
        }

        public T FindFirst(Expression<Func<T, bool>> expression, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;
            if (expression != null)
                query = query.Where(expression);

            foreach (var includeProperty in includeProperties
                     .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }

            return query.AsNoTracking().FirstOrDefault();
            //return this.akshadaPawsContext.Set<T>().Where(expression).FirstOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            return this.akshadaPawsContext.Set<T>();
        }

        public IQueryable<T> GetAllWithInclude(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
                query = query.Where(filter);

            // comma-separated includes: "Pets,Orders,Orders.Items"
            foreach (var includeProperty in includeProperties
                         .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }

            if (orderBy != null)
                return orderBy(query);

            return query;
        }

        public T GetByGuid(string rowId)
        {
            return this.akshadaPawsContext.Set<T>().Find(rowId);
        }

        public void Remove(T entity)
        {
            this.akshadaPawsContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            this.akshadaPawsContext.Set<T>().Update(entity);
        }

        public bool RemoveIncludingChildren(string includes, Expression<Func<T, bool>> predicate)
        {
            var query = akshadaPawsContext.Set<T>().AsQueryable();
            List<string> includeList = new();

            // Split and apply includes
            if (!string.IsNullOrWhiteSpace(includes))
            {
                includeList = includes.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                      .Select(i => i.Trim()).ToList();

                foreach (var include in includeList)
                {
                    query = query.Include(include);
                }
            }

            var entity = query.FirstOrDefault(predicate);
            if (entity == null)
                return false;

            // Recursively delete children and grandchildren
            RemoveEntityWithIncludes(entity, includeList);

            // Finally remove parent
            akshadaPawsContext.Remove(entity);

            return true;
        }

        private void RemoveEntityWithIncludes(object entity, List<string> includeList)
        {
            var entry = akshadaPawsContext.Entry(entity);

            foreach (var navigation in entry.Navigations)
            {
                var navName = navigation.Metadata.Name;

                // Match includes that start with this nav name
                var matchingIncludes = includeList
                    .Where(i => i.StartsWith(navName, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (!matchingIncludes.Any())
                    continue;

                // Load if not loaded
                if (!navigation.IsLoaded)
                    navigation.Load();

                if (navigation.Metadata.IsCollection)
                {
                    var children = (IEnumerable<object>)navigation.CurrentValue;
                    if (children != null)
                    {
                        // Nested includes, e.g., "Details.DetailItems" → ["DetailItems"]
                        var childIncludes = matchingIncludes
                            .Where(i => i.Contains('.'))
                            .Select(i => i.Substring(i.IndexOf('.') + 1))
                            .ToList();

                        foreach (var child in children.ToList())
                        {
                            RemoveEntityWithIncludes(child, childIncludes);
                        }

                        akshadaPawsContext.RemoveRange(children);
                    }
                }
                else
                {
                    var child = navigation.CurrentValue;
                    if (child != null)
                    {
                        var childIncludes = matchingIncludes
                            .Where(i => i.Contains('.'))
                            .Select(i => i.Substring(i.IndexOf('.') + 1))
                            .ToList();

                        RemoveEntityWithIncludes(child, childIncludes);
                        akshadaPawsContext.Remove(child);
                    }
                }
            }
        }

        public IQueryable<T> FromSqlRawAsync(string sql, params object[] parameters)
        {
            return _dbSet.FromSqlRaw(sql, parameters).AsQueryable();
        }

        public void Remove(IEnumerable<T> entities)
        {
            akshadaPawsContext.Set<T>().RemoveRange(entities);
        }
    }
}
