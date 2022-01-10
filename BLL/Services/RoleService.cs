using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models.Account;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<UserProfile> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(UserManager<UserProfile> userManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        
        public async Task AssignUserToRoles(AssignUserToRoles assignUserToRoles)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == assignUserToRoles.Email);
            var roles = _roleManager.Roles.ToList().Where(r => assignUserToRoles.Roles.Contains(r.Name, StringComparer.OrdinalIgnoreCase))
                .Select(r => r.NormalizedName).ToList();

            var result = await _userManager.AddToRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                throw new System.Exception(string.Join(';', result.Errors.Select(x => x.Description)));
            }
        }
        
        public async Task CreateRole(string roleName)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (!result.Succeeded)
            {
                throw new System.Exception($"Role could not be created: {roleName}.");
            }
        }
        
        /// <summary>
        /// This method returns all users roles in string. I need this method for JWT.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>All users roles in string format</returns>
        public async Task<IEnumerable<string>> GetRoles(UserProfile user)
        {
            return await _userManager.GetRolesAsync(user);
        }
        
        public IEnumerable<IdentityRole> GetRoles()
        {
            return _roleManager.Roles.ToList();
        }

        public async Task<List<string>> GetRole(string userEmail)
        {
            /*var users = await _unitOfWork.UserRepository.GetAllAsync();
            var email = users.FirstOrDefault(u => u.Id == id).Email;*/
            var user = _userManager.Users.FirstOrDefault(u=>u.UserName==userEmail);
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
    }
}