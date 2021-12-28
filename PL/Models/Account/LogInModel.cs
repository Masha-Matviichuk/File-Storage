using System.ComponentModel.DataAnnotations;

namespace PL.Models.Account
{
    public class LogInModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}