using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class FileStorageDBContext :  DbContext, IFileStorageDBContext
    {
        //IdentityDbContext<UserProfile>
        public FileStorageDBContext(DbContextOptions<FileStorageDBContext> options) : base(options)
        {
           
        }

        public FileStorageDBContext()
        {
        }

        public DbSet<File> Files { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Access> Accesses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=.;Database=FileStorageDB;Trusted_Connection=True;");
        }
    }
}