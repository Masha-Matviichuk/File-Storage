using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;
        private readonly UserManager<UserProfile> _userManager;

        public UserService(IUnitOfWork unitOfWork, IMapper autoMapper)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
        }

        public async Task<IEnumerable<UserInfoDto>> GetAllAsync()
        {
            var entities = await _unitOfWork.UserRepository.GetAllAsync();
            return _autoMapper.Map<IEnumerable<UserInfoDto>>(entities);
        }

        public async Task<UserInfoDto> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.UserRepository.GetByIdAsync(id);
            return _autoMapper.Map<UserInfoDto>(entity);
        }

        /*public async Task AddAsync(UserInfoDto model)
        { 
            var entity = _autoMapper.Map<User>(model);
            await _unitOfWork.UserRepository.CreateAsync(entity);
        }*/

        public async Task UpdateAsync(UserInfoDto model)
        {
            var entity = _autoMapper.Map<User>(model);
            await _unitOfWork.UserRepository.UpdateAsync(entity);
        }

        //Add Delete for Profile, if i need it 
        public async Task DeleteByIdAsync(int id)
        {
            await _unitOfWork.UserRepository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<FileDto>> GetAllUsersFiles(int userId)
        {
            var files = await _unitOfWork.FileRepository.GetAllAsync();
            var userFiles = files.Where(f => f.UserId == userId);
            //var user =await _unitOfWork.UserRepository.GetByIdAsync(userId);
            return _autoMapper.Map<IEnumerable<FileDto>>(userFiles);
        }
        
        /*public async Task<IEnumerable<FileDto>> GetAllUsersFiles(ClaimsPrincipal user)
        {
            var userName = _userManager.GetUserName(user);
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            var userId = users.FirstOrDefault(u => u.Email == userName).Id;
            var files = await _unitOfWork.FileRepository.GetAllAsync();
            var userFiles = files.Where(f => f.UserId == userId);
            //var user =await _unitOfWork.UserRepository.GetByIdAsync(userId);
            return _autoMapper.Map<IEnumerable<FileDto>>(userFiles);
        }*/
      
        /*public async Task<FileDto> GetUserFileById(int userId, int fileId)
        {
            var user =await _unitOfWork.UserRepository.GetByIdAsync(userId);
            var file = await _unitOfWork.FileRepository.GetAllAsync();
            file.Where()
            return _autoMapper.Map<IEnumerable<FileDto>>(user.Files);
        }*/
    }
}