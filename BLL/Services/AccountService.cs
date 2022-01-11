using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth;
using Auth.Entities;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Models.Account;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<UserProfile> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(UserManager<UserProfile> userManager,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task SignUp(SignUpDto data)
        {
            var userInfo = _mapper.Map<UserProfile>(data);
            var user = _mapper.Map<User>(data);
            
            var result = await _userManager.CreateAsync(userInfo
            , data.Password);
            
            if (!result.Succeeded)
            {
                throw new System.Exception(string.Join(';', result.Errors.Select(x => x.Description)));
            }
            var newUser = await _unitOfWork.UserRepository.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UserProfile> LogIn(LogInDto data)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == data.Email);
            if (user is null) throw new System.Exception($"User not found: '{data.Email}'.");

            return await _userManager.CheckPasswordAsync(user, data.Password) ? user : null;
        }
        
       

        public async Task DeleteAccount(int id)
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            var userEmail = users.FirstOrDefault(u => u.Id == id)?.Email;

            if (userEmail == null) throw new NullReferenceException(nameof(userEmail));

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userEmail);
                await _unitOfWork.UserRepository.DeleteByIdAsync(id);
                await _userManager.DeleteAsync(user);
                await _unitOfWork.SaveChangesAsync();
        }
        
    }
}