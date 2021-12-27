using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public class UserProfile : IdentityUser
    { 
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}