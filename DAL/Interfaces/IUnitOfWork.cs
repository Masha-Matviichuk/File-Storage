
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<File> FileRepository { get;  }
        IRepository<User> UserRepository { get; }
        IFileStorageRepository FileStorageRepository { get; }
        IFileAccessRepository FileAccessRepository { get; }
        Task<int> SaveChangesAsync();
    }
}