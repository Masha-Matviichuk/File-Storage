using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class File : BaseEntity
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        
        [ForeignKey(nameof(Access))]
        public  int AccessId { get; set; }
        
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public DateTime Upload { get; set; }
        public long Size { get; set; }
        public virtual User User { get; set; }
        public virtual Access Access { get; set; }

    }
}