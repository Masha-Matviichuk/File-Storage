using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public virtual ICollection<File> Files { get; set; }
        //public virtual UserProfile UserProfile { get; set; }

       
    }
}