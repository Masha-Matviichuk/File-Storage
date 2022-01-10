using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IFileRepository FileRepository { get;  }
        IUserRepository UserRepository { get; }
        IFileStorageRepository FileStorageRepository { get; }
        IFileAccessRepository FileAccessRepository { get; }

      
        Task<int> SaveChangesAsync();
    }
}