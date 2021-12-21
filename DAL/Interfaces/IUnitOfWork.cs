using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IFileRepository FileRepository { get;  }
        IUserRepository UserRepository { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}