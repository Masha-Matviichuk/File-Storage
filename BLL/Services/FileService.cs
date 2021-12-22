using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class FileService:IFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _autoMapper;

        public FileService(IUnitOfWork unitOfWork, Mapper autoMapper)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
        }
        public async Task<IEnumerable<FileModel>> GetAllAsync()
        {
            var entities = await _unitOfWork.FileRepository.GetAllAsync();
            return _autoMapper.Map<IEnumerable<FileModel>>(entities);
        }

        public async Task<FileModel> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.FileRepository.GetByIdAsync(id);
            return _autoMapper.Map<FileModel>(entity);
        }

        public async Task AddAsync(FileModel model)
        { 
            var entity = _autoMapper.Map<File>(model);
            await _unitOfWork.FileRepository.CreateAsync(entity);
        }

        public async Task UpdateAsync(FileModel model)
        {
            var entity = _autoMapper.Map<File>(model);
            await _unitOfWork.FileRepository.UpdateAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _unitOfWork.FileRepository.DeleteByIdTask(id);
        }
    }
}