using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _autoMapper;

        public UserService(IUnitOfWork unitOfWork, Mapper autoMapper)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
        }

        public async Task<IEnumerable<UserInfoModel>> GetAllAsync()
        {
            var entities = await _unitOfWork.UserRepository.GetAllAsync();
            return _autoMapper.Map<IEnumerable<UserInfoModel>>(entities);
        }

        public async Task<UserInfoModel> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.UserRepository.GetByIdAsync(id);
            return _autoMapper.Map<UserInfoModel>(entity);
        }

        public async Task AddAsync(UserInfoModel model)
        { 
            var entity = _autoMapper.Map<User>(model);
            await _unitOfWork.UserRepository.CreateAsync(entity);
        }

        public async Task UpdateAsync(UserInfoModel model)
        {
            var entity = _autoMapper.Map<User>(model);
            await _unitOfWork.UserRepository.UpdateAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _unitOfWork.UserRepository.DeleteByIdTask(id);
        }
    }
}