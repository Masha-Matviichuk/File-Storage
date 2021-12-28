using System.ComponentModel.DataAnnotations;

namespace PL.Models.Account
{
    public class CreateRoleModel
    {
        [Required, MinLength(5), MaxLength(20)]
        public string RoleName { get; set; }
    }
}