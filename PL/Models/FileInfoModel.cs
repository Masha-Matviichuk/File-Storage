using System;

namespace PL.Models
{
    public class FileInfoModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public  string Access { get; set; }
        public int UserId { get; set; }
        public DateTime Upload { get; set; }
        public long Size { get; set; }
    }
}