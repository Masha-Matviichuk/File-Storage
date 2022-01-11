namespace BLL.Models.Account
{
    public class AssignUserToRolesDto
    {
        public string Email { get; set; }
        public string[] Roles { get; set; }
    }
}