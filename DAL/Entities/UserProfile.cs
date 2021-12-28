using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public class UserProfile : IdentityUser
    { 
       
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}