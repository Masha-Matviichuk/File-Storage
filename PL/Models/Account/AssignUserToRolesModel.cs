using System.ComponentModel.DataAnnotations;

namespace PL.Models.Account
{
    public class AssignUserToRolesModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string[] Roles { get; set; }
    }
}