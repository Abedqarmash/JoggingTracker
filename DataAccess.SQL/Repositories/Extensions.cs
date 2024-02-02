using Contracts.V1.SharedModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.SQL.Repositories
{
    public static class Extensions
    {

        public static IQueryable<TEntity> ApplyFilters<TEntity>(this IQueryable<TEntity> entities,
            IEnumerable<Expression<Func<TEntity, bool>>>? expressions = null)
            where TEntity : class
        {
            if (expressions is null)
            {
                return entities;
            }

            return expressions.Aggregate(entities, (current, filter) => filter != null ? current.Where(filter) : current);
        }


        public static IQueryable<TEntity> ApplyIncludes<TEntity>(
            this IQueryable<TEntity> entities,
            IEnumerable<Expression<Func<TEntity, object>>>? includes = null)
            where TEntity : class
        {
            if (includes is null)
            {
                return entities;
            }

            foreach (var include in includes)
            {
                if (include is not null)
                {
                    entities = entities.Include(include);
                }
            }
            return entities;
        }

        public static IQueryable<TEntity> ApplyPaging<TEntity>(this IQueryable<TEntity> entities, BaseFilter? filter = null)
        {
            if (filter is null)
            {
                return entities;
            }
            return entities.Skip(filter.Skip ?? 0).Take(filter.Take ?? 1000);
        }
    }
}
