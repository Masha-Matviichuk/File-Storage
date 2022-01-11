using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth;
using Auth.Entities;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;
        private readonly UserManager<UserProfile> _userManager;

        public UserService(IUnitOfWork unitOfWork, IMapper autoMapper, UserManager<UserProfile> userManager)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserInfoDto>> GetAllAsync()
        {
            var entities = await _unitOfWork.UserRepository.GetAllAsync();
            return _autoMapper.Map<IEnumerable<UserInfoDto>>(entities);
        }

        public async Task<UserInfoDto> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.UserRepository.GetByIdAsync(id);

            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var userInfo = _autoMapper.Map<UserInfoDto>(entity);
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == userInfo.Email);
            var role = await _userManager.GetRolesAsync(user);
            userInfo.FirstName = user.FirstName;
            userInfo.LastName = user.LastName;
            userInfo.PhoneNumber = user.PhoneNumber;
            userInfo.Roles = role;

            return userInfo;
        }

        public async Task UserBan(int id, int days)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null) throw new NullReferenceException(nameof(user));

            user.IsBanned = true;
            user.EndOfBan = DateTime.Now.AddDays(days);

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> CheckBan(string userEmail, DateTime presentTime)
        {
            var currentUser = (await _unitOfWork.UserRepository.GetAllAsync()).FirstOrDefault(u=>u.Email==userEmail);
            if (currentUser == null) throw new NullReferenceException(nameof(currentUser));
                
            var user = await _unitOfWork.UserRepository.GetByIdAsync(currentUser.Id);
            if (user == null) throw new NullReferenceException(nameof(user));
            
           if (user.EndOfBan < presentTime)
           {
                user.IsBanned = false;
                return false; 
           } 
           return true;

        }

        public async Task UpdateAsync(UserInfoDto model)
        {
            var entity = _autoMapper.Map<User>(model);
            await _unitOfWork.UserRepository.UpdateAsync(entity);
        }
    }
}