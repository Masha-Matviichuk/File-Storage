using System;
using DAL.Entities;


namespace BLL.Models
{
    public class FileDto : BaseEntityDto
    {
        public string Title { get; set; }
        public string Url { get; set; }
        
        public string Description { get; set; }
        public DateTime Upload { get; set; }
        public long Size { get; set; }
        public User User { get; set; }
        public Access AccessToFile { get; set; }
    }
}