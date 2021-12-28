using System.Collections.Generic;

namespace PL.Models
{
    public class UserInfoModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        
        public  ICollection<int> FileIds{ get; set; }
    }
}