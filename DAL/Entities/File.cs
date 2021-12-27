using System;

namespace DAL.Entities
{
    public class File : BaseEntity
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public  int AccessId { get; set; }
        public DateTime Upload { get; set; }
        public long Size { get; set; }
        public virtual User User { get; set; }
        public virtual Access Access { get; set; }

    }
}