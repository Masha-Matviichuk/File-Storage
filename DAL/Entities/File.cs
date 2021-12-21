using System;
using DAL.Enums;

namespace DAL.Entities
{
    public class File : BaseEntity
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public Access AccessToFile { get; set; }
        public string Description { get; set; }
        public DateTime Upload { get; set; }
        public long Size { get; set; }
        public virtual User User { get; set; }
        
    }
}