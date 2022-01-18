using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Entities;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
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

        public async Task<bool> CheckBan(string userEmail)
        {
            var currentUser = (await _unitOfWork.UserRepository.GetAllAsync()).FirstOrDefault(u=>u.Email==userEmail);
            if (currentUser == null) throw new NullReferenceException(nameof(currentUser));
                
            var entity = await _unitOfWork.UserRepository.GetByIdAsync(currentUser.Id);
            if (entity == null) throw new NullReferenceException(nameof(entity)); 
            if (entity.EndOfBan < DateTime.Now)
            {
                entity.IsBanned = false;
                await _unitOfWork.UserRepository.UpdateAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                return false; 
            } 
            return true;
        }
    }
}