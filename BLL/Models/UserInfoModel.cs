using System.Collections.Generic;

namespace BLL.Models
{
    public class UserInfoModel : BaseEntityModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public  ICollection<int> FileIds{ get; set; }
    }
}