using Microsoft.AspNetCore.Identity;

namespace Auth
{
    public class UserProfile : IdentityUser
    { 
       
        //[ForeignKey(nameof(User))]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
       // public virtual User User { get; set; }
    }
}