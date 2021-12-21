using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class FileStorageDBContext : DbContext
    {
        public DbSet<File> Files { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UsersProfiles { get; set; }
    }
}