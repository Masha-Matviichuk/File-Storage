using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Models.Account;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Interfaces
{
    public interface IAccountService
    {
        Task SignUp(SignUp data);
        Task<User> LogIn(LogIn data);
        Task AssignUserToRoles(AssignUserToRoles assignUserToRoles);
        Task CreateRole(string roleName);
        Task<IEnumerable<string>> GetRoles(User user);
        Task<IEnumerable<IdentityRole>> GetRoles();
    }
}