using System.Collections.Generic;

namespace DAL.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<File> Files { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}