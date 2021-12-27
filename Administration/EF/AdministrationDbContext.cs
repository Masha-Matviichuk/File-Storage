using Administration.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Administration.EF
{
    public class AdministrationDbContext : IdentityDbContext<ApplicationUser>
    {
        
    }
}