using Contracts.V1.SharedModels;
using System.Linq.Expressions;

namespace DataAccess.SQL.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetItemsAsync(
            IEnumerable<Expression<Func<TEntity, bool>>> expressions,
            IEnumerable<Expression<Func<TEntity, object>>>? includes = null,
            BaseFilter? pagingFilter = null);

        Task<int> CountAsync(IEnumerable<Expression<Func<TEntity, bool>>> expressions);

        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, IEnumerable<Expression<Func<TEntity, object>>>? includes = null);

        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
