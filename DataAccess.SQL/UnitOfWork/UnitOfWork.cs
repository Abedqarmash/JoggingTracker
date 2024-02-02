using DataAccess.SQL.ApplicationDbContext;
using DataAccess.SQL.Entities;
using DataAccess.SQL.Repositories;

namespace DataAccess.SQL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IRepository<UserEntity> UsersRepository { get; }

        public Task SaveChanges();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private IRepository<UserEntity> _usersRepoistory = default!;

        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext appDbContext) => _context = appDbContext;

        public IRepository<UserEntity> UsersRepository
        {
            get
            {
                return _usersRepoistory ??= new BaseRepository<UserEntity>(_context);
            }
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
