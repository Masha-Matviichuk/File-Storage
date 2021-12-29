using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public class User : BaseEntity
    {
        public string NickName { get; set; }
       
        public virtual ICollection<File> Files { get; set; }
        //public virtual UserProfile UserProfile { get; set; }

       
    }
}