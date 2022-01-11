using Auth.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth
{
    public class AdministrationDBContext : IdentityDbContext<UserProfile>
    {
        public AdministrationDBContext(DbContextOptions<AdministrationDBContext> options) : base(options)
        {
           
        }

        public AdministrationDBContext()
        {
        }
        
        public DbSet<UserProfile> UserProfiles { get; set; }

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
            //optionsBuilder.UseSqlServer(@"Server=.;Database=AdministrationDB;Trusted_Connection=True;");
        }
    }
}