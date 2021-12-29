namespace BLL.Models
{
    public class UserProfileDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
    }
}