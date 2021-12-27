using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
    }
}