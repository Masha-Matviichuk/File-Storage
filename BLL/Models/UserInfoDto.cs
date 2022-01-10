using System.Collections.Generic;

namespace BLL.Models
{
    public class UserInfoDto : BaseEntityDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsBanned { get; set; }
        public ICollection<string> Roles { get; set; }

        public  ICollection<int> FileIds{ get; set; }
    }
}