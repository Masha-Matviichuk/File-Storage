using System;
using DAL.Entities;
using DAL.Enums;

namespace BLL.Models
{
    public class FileModel : BaseEntityModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public Access AccessToFile { get; set; }
        public string Description { get; set; }
        public DateTime Upload { get; set; }
        public long Size { get; set; }
        public User User { get; set; }
    }
}