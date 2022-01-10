using System;
using System.Security.Claims;
using DAL.Entities;


namespace BLL.Models
{
    public class FileDto : BaseEntityDto
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public int AccessId { get; set; }
        public string Extension { get; set; }
        public DateTime Upload { get; set; }
        public long Size { get; set; }
        public ClaimsPrincipal CurrentUser { get; set; }
        public Access Access { get; set; }
    }
}