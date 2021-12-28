using System.ComponentModel.DataAnnotations;

namespace PL.Models.Account
{
    public class AssignUserToRolesModel
    {
        [Required]
        public string Email { get; set; }
        [Required, MinLength(1)]
        public string[] Roles { get; set; }
    }
}