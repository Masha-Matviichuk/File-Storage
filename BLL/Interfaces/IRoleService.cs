using System.Collections.Generic;
using System.Threading.Tasks;
using Auth;
using BLL.Models.Account;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Interfaces
{
    public interface IRoleService
    {
        Task AssignUserToRoles(AssignUserToRoles assignUserToRoles);
        Task CreateRole(string roleName);
        Task<IEnumerable<string>> GetRoles(UserProfile user);
        IEnumerable<IdentityRole> GetRoles();
        Task<List<string>> GetRole(string userEmail);
    }
}


