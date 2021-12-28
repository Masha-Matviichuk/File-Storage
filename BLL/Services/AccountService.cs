using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Models.Account;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Mapper _mapper;

        public AccountService(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            Mapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        //I think better add dto to SignUp
        public async Task SignUp(SignUp data)
        {
            var userInfo = _mapper.Map<User>(data);
            
            var result = await _userManager.CreateAsync(userInfo
            , data.Password);

            if (!result.Succeeded)
            {
                throw new System.Exception(string.Join(';', result.Errors.Select(x => x.Description)));
            }
        }

        public async Task<User> LogIn(LogIn data)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserProfile.UserName == data.Email);
            if (user is null) throw new System.Exception($"User not found: '{data.Email}'.");

            return await _userManager.CheckPasswordAsync(user, data.Password) ? user : null;
        }
        
        /*public async Task<User> LogOut(LogIn data)
        {
            _u
            var user = _userManager.Users.SingleOrDefault(u => u.UserProfile.UserName == data.Email);
            if (user is null) throw new System.Exception($"User not found: '{data.Email}'.");

            return await _userManager.CheckPasswordAsync(user, data.Password) ? user : null;
        }*/

        public async Task AssignUserToRoles(AssignUserToRoles assignUserToRoles)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserProfile.UserName == assignUserToRoles.Email);
            var roles = _roleManager.Roles.ToList().Where(r => assignUserToRoles.Roles.Contains(r.Name, StringComparer.OrdinalIgnoreCase))
                .Select(r => r.NormalizedName).ToList();

            var result = await _userManager.AddToRolesAsync(user, roles); // THROWS

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

        public async Task<IEnumerable<string>> GetRoles(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IEnumerable<IdentityRole>> GetRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }
    }
}