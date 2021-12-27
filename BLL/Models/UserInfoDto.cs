using System.Collections.Generic;

namespace BLL.Models
{
    public class UserInfoDto : BaseEntityDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Pssword { get; set; }
        public string Phone { get; set; }
        
        public  ICollection<int> FileIds{ get; set; }
    }
}