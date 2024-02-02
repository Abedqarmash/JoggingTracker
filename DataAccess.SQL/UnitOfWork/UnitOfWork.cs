using DataAccess.SQL.ApplicationDbContext;
using DataAccess.SQL.Entities;
using DataAccess.SQL.Repositories;

namespace DataAccess.SQL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IRepository<UserEntity> UsersRepository { get; }
        public IRepository<JoggingEntity> JoggingRepository { get; }

        public Task SaveChanges();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private IRepository<UserEntity> _usersRepoistory = default!;
        private IRepository<JoggingEntity> _joggingRepoistory = default!;

        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext appDbContext) => _context = appDbContext;

        public IRepository<UserEntity> UsersRepository
        {
            get
            {
                return _usersRepoistory ??= new BaseRepository<UserEntity>(_context);
            }
        }


        public IRepository<JoggingEntity> JoggingRepository
        {
            get
            {
                return _joggingRepoistory ??= new BaseRepository<JoggingEntity>(_context);
            }
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
