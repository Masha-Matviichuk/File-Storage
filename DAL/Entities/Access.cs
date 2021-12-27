using System.Collections.Generic;

namespace DAL.Entities
{
    public class Access : BaseEntity
    {
        public string Modifier { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}