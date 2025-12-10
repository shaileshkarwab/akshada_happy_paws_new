using Akshada.DTO.Helpers;
using Akshada.DTO.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Services
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyEqualityFilters<T>(this IQueryable<T> query, List<Filter> filters)
        {
            if (filters == null || filters.Count == 0)
                return query;

            var parameter = Expression.Parameter(typeof(T), "x");
            Expression? combined = null;

            foreach (var filter in filters)
            {
                Expression? propertyExpr = null;

                // Case 1: Root entity property
                if (string.Equals(filter.EntityName, typeof(T).Name, StringComparison.OrdinalIgnoreCase))
                {
                    var prop = typeof(T).GetProperty(filter.DbColumnName);
                    if (prop == null)
                        continue;

                    propertyExpr = Expression.Property(parameter, prop);
                }
                else
                {
                    // Case 2: Included navigation entity
                    var navProp = typeof(T).GetProperties()
                        .FirstOrDefault(p =>
                            string.Equals(p.Name, filter.EntityName, StringComparison.OrdinalIgnoreCase));

                    if (navProp != null)
                    {
                        var navExpr = Expression.Property(parameter, navProp);
                        var innerProp = navProp.PropertyType.GetProperty(filter.DbColumnName);
                        if (innerProp == null)
                            continue;

                        propertyExpr = Expression.Property(navExpr, innerProp);
                    }
                }

                if (propertyExpr == null)
                    continue;

                // Ensure it’s a MemberExpression to get the property type
                if (propertyExpr is not MemberExpression memberExpr)
                    continue;

                var propertyType = ((PropertyInfo)memberExpr.Member).PropertyType;
                var targetType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

                object? typedValue;
                try
                {
                    typedValue = Convert.ChangeType(filter.Value, targetType);
                }
                catch
                {
                    continue; // skip invalid conversions
                }

                var right = Expression.Constant(typedValue, propertyType);
                var equalExpr = Expression.Equal(propertyExpr, right);

                combined = combined == null ? equalExpr : Expression.AndAlso(combined, equalExpr);
            }

            if (combined == null)
                return query;

            var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);
            return query.Where(lambda);
        }

        public static IQueryable<T> ApplyAdvanceFilters<T>(this IQueryable<T> query, DTO_FilterAndPaging filterAndPaging)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression? combined = null;

            static Expression? GetPropertyExpression(ParameterExpression parameter, string entityName, string propertyName)
            {

                if (!string.Equals(entityName, typeof(T).Name, StringComparison.OrdinalIgnoreCase))
                    return null;

                // Split the nested path like "RequiredServiceSystem.RowId"
                var parts = propertyName.Split('.', StringSplitOptions.RemoveEmptyEntries);
                Expression? propertyExpr = parameter;
                Type currentType = typeof(T);

                foreach (var part in parts)
                {
                    var prop = currentType.GetProperty(part,
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    if (prop == null)
                        return null;

                    propertyExpr = Expression.Property(propertyExpr, prop);
                    currentType = prop.PropertyType;
                }

                return propertyExpr;




                //if (string.Equals(entityName, typeof(T).Name, StringComparison.OrdinalIgnoreCase))
                //{
                //    var prop = typeof(T).GetProperty(propertyName);
                //    return prop != null ? Expression.Property(parameter, prop) : null;
                //}

                //var navProp = typeof(T).GetProperties()
                //    .FirstOrDefault(p => string.Equals(p.Name, entityName, StringComparison.OrdinalIgnoreCase));
                //if (navProp == null) return null;

                //var navExpr = Expression.Property(parameter, navProp);
                //var innerProp = navProp.PropertyType.GetProperty(propertyName);
                //return innerProp != null ? Expression.Property(navExpr, innerProp) : null;
            }

            // equality filter
            if (filterAndPaging.EqualityFilters != null && filterAndPaging.EqualityFilters.Count > 0)
            {
                foreach (var filter in filterAndPaging.EqualityFilters)
                {
                    var expr = GetPropertyExpression(parameter, filter.EntityName, filter.DbColumnName);
                    if (expr is not MemberExpression memberExpr)
                        continue;

                    var propertyType = ((PropertyInfo)memberExpr.Member).PropertyType;
                    var targetType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
                    object? typedValue = Convert.ChangeType(filter.Value, targetType);

                    var right = Expression.Constant(typedValue, propertyType);
                    var equal = Expression.Equal(expr, right);
                    combined = combined == null ? equal : Expression.AndAlso(combined, equal);
                }
            }

            //contains
            // Criteria filters (string Contains)
            if (filterAndPaging.Criteria != null && filterAndPaging.Criteria.Count > 0)
            {
                foreach (var filter in filterAndPaging.Criteria)
                {
                    var expr = GetPropertyExpression(parameter, filter.EntityName, filter.DbColumnName);
                    if (expr is not MemberExpression memberExpr)
                        continue;

                    if (memberExpr.Type != typeof(string))
                        continue;

                    var valueExpr = Expression.Constant(filter.Value.ToLower());
                    var toLowerCall = Expression.Call(expr, typeof(string).GetMethod("ToLower", Type.EmptyTypes)!);
                    var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
                    var containsExpr = Expression.Call(toLowerCall, containsMethod, valueExpr);

                    combined = combined == null ? containsExpr : Expression.AndAlso(combined, containsExpr);
                }
            }

            //boolean filer
            if (filterAndPaging.BooleanFilters != null && filterAndPaging.BooleanFilters.Count > 0)
            {
                foreach (var filter in filterAndPaging.BooleanFilters)
                {
                    var expr = GetPropertyExpression(parameter, filter.EntityName, filter.DbColumnName);
                    if (expr is not MemberExpression memberExpr)
                        continue;

                    var propertyType = ((PropertyInfo)memberExpr.Member).PropertyType;
                    var targetType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
                    object? typedValue = Convert.ChangeType(filter.Value, targetType);

                    var right = Expression.Constant(typedValue, propertyType);
                    var equal = Expression.Equal(expr, right);
                    combined = combined == null ? equal : Expression.AndAlso(combined, equal);
                }
            }

            //date filters
            if (filterAndPaging.DateFilters != null && filterAndPaging.DateFilters.Count > 0)
            {
                foreach (var filter in filterAndPaging.DateFilters)
                {
                    if (!string.IsNullOrEmpty(filter.FromValue))
                    {
                        var fromDateExpr = GetPropertyExpression(
                        parameter,
                        filter.EntityName,
                        filter.DbColumnName);

                        if (fromDateExpr != null)
                        {
                            var fromDateValue = DateTimeHelper.GetDate(filter.FromValue).Date;
                            var constant = Expression.Constant(fromDateValue, typeof(DateTime));
                            var greaterThanOrEqual = Expression.GreaterThanOrEqual(fromDateExpr, constant);
                            combined = combined == null ? greaterThanOrEqual : Expression.AndAlso(combined, greaterThanOrEqual);
                        }
                    }


                    if (!string.IsNullOrEmpty(filter.ToValue))
                    {
                        var fromDateExpr = GetPropertyExpression(
                        parameter,
                        filter.EntityName,
                        filter.DbColumnName);

                        if (fromDateExpr != null)
                        {
                            var fromDateValue = DateTimeHelper.GetDate(filter.ToValue);
                            var constant = Expression.Constant(fromDateValue, typeof(DateTime));
                            var greaterThanOrEqual = Expression.LessThanOrEqual(fromDateExpr, constant);
                            combined = combined == null ? greaterThanOrEqual : Expression.AndAlso(combined, greaterThanOrEqual);
                        }
                    }
                }
            }



            // not equality filter
            if (filterAndPaging.NotEqualityFilters != null && filterAndPaging.NotEqualityFilters.Count > 0)
            {
                foreach (var filter in filterAndPaging.NotEqualityFilters)
                {
                    var expr = GetPropertyExpression(parameter, filter.EntityName, filter.DbColumnName);
                    if (expr is not MemberExpression memberExpr)
                        continue;

                    var propertyType = ((PropertyInfo)memberExpr.Member).PropertyType;
                    var targetType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
                    object? typedValue = Convert.ChangeType(filter.Value, targetType);

                    var right = Expression.Constant(typedValue, propertyType);
                    var equal = Expression.NotEqual(expr, right);
                    combined = combined == null ? equal : Expression.AndAlso(combined, equal);
                }
            }

            if (combined == null)
                return query;

            var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);
            return query.Where(lambda);
        }
    }
}
