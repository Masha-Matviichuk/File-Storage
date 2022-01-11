using System.ComponentModel.DataAnnotations;

namespace PL.Models.Account
{
    public class SignUpModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }
        [Required, MinLength(8)]
        [Compare(nameof(Password))]
        public string PasswordConfirm { get; set; }
        
    }
}