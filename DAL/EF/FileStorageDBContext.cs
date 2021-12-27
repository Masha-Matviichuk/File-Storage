using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class FileStorageDBContext :  IdentityDbContext<UserProfile>, IFileStorageDBContext
    {
        
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
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(new[]
            {
                new IdentityRole("user"),
                new IdentityRole("admin")
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=FileStorageDB;Trusted_Connection=True;");
        }
    }
}