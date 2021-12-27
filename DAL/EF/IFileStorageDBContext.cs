using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public interface IFileStorageDBContext
    {
        DbSet<T> Set<T>() where T: class;
    }
}