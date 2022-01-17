using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public bool IsBanned { get; set; }
        public DateTime EndOfBan { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}