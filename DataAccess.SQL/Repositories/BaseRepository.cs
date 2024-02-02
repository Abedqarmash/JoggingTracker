using Contracts.V1.SharedModels;
using DataAccess.SQL.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.SQL.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;

        public BaseRepository(AppDbContext context) => _context = context;

        public async Task<List<TEntity>> GetItemsAsync(
            IEnumerable<Expression<Func<TEntity, bool>>> expressions,
            IEnumerable<Expression<Func<TEntity, object>>>? includes = null,
            BaseFilter? pagingFilter = null)
        {
            return await _context
                .Set<TEntity>()
                .AsNoTracking()
                .ApplyIncludes(includes)
                .ApplyFilters(expressions)
                .ApplyPaging(pagingFilter)
                .ToListAsync();
        }

        public async Task<int> CountAsync(IEnumerable<Expression<Func<TEntity, bool>>> expressions)
        {
            return await _context
                .Set<TEntity>()
                .AsNoTracking()
                .ApplyFilters(expressions)
                .CountAsync();
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, IEnumerable<Expression<Func<TEntity, object>>>? includes = null)
        {
            return await _context
                .Set<TEntity>()
                .AsNoTracking()
                .ApplyIncludes(includes)
                .FirstOrDefaultAsync(expression);
        }

        public void Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
