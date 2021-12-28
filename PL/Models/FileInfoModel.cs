using System;

namespace PL.Models
{
    public class FileInfoModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public  int AccessId { get; set; }
        public int UserId { get; set; }
        public DateTime Upload { get; set; }
        public long Size { get; set; }
    }
}