using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Repositories;

namespace DAL.UoW
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private readonly FileStorageDBContext _context;
        private IFileRepository _fileRepository;
        private IUserRepository _userRepository;

        public UnitOfWork(FileStorageDBContext context)
        {
            _context = context;
        }

        public  IFileRepository FileRepository
        {
            get
            {
                if (_fileRepository==null)
                {
                    _fileRepository = new FileRepository(_context);
                }

                return _fileRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository==null)
                {
                    _userRepository= new UserRepository(_context);
                }

                return _userRepository;
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}