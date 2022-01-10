using System.Threading.Tasks;
using DAL.EF;
using DAL.Interfaces;
using DAL.Repositories;

namespace DAL.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FileStorageDBContext _context;
        private IFileRepository _fileRepository;
        private IUserRepository _userRepository;
        private IFileAccessRepository _fileAccessRepository;
        private IFileStorageRepository _storage;

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

        public IFileStorageRepository FileStorageRepository
        {
            get
            {
                if (_storage==null)
                {
                    _storage = new FileStorageRepository();
                }

                return _storage;
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
        
        public IFileAccessRepository FileAccessRepository
        {
            get
            {
                if (_fileAccessRepository==null)
                {
                    _fileAccessRepository= new FileAccessRepository(_context);
                }

                return _fileAccessRepository;
            }
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}