using System.Collections.Generic;

namespace BLL.Models
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
       
        public virtual ICollection<FileDto> Files { get; set; }
        public virtual UserProfileDto UserProfile { get; set; }
    }
}