using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class FileStorageDBContext :  DbContext, IFileStorageDBContext
    {
        public FileStorageDBContext(DbContextOptions<FileStorageDBContext> options) : base(options)
        {
           
        }

        public DbSet<File> Files { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Access> Accesses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}