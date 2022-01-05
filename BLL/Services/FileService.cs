using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Auth;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using File = DAL.Entities.File;

namespace BLL.Services
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;
        private readonly UserManager<UserProfile> _userManager;


        public FileService(IUnitOfWork unitOfWork, IMapper autoMapper, UserManager<UserProfile> userManager)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
            _userManager = userManager;
        }
        public async Task<IEnumerable<FileDto>> GetAllAsync()
        {
            var entities = await _unitOfWork.FileRepository.GetAllAsync();
            return _autoMapper.Map<IEnumerable<FileDto>>(entities);
        }

        public async Task<FileDto> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.FileRepository.GetByIdAsync(id);
            return _autoMapper.Map<FileDto>(entity);
        }

     
        public async Task<File> AddAsync(Stream fileStream, FileDto model)
        {
            var path = await _unitOfWork.FileStorageRepository.CreateAsync(fileStream, model.Title, model.Extension);
            var userEmail =  _userManager.GetUserName(model.CurrentUser);
            var users = await _unitOfWork.UserRepository.GetAllAsync();

            var file = _autoMapper.Map<DAL.Entities.File>(model);
            file.Url = path;
            file.Size = _unitOfWork.FileStorageRepository.GetInfo(path).Length;
            file.Upload = DateTime.Now;
            file.UserId = users.FirstOrDefault(u => u.Email == userEmail).Id;

            var result =await _unitOfWork.FileRepository.CreateAsync(file);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<File> UpdateAsync(Stream fileStream, FileDto model)
        {
            var file = await _unitOfWork.FileRepository.GetByIdAsync(model.Id);
            if (fileStream != null)
            {
                _unitOfWork.FileStorageRepository.Delete(file.Url);
                 var path = await _unitOfWork.FileStorageRepository.CreateAsync(fileStream, model.Title, model.Extension);
                 file.Url = path;
                 file.Size = _unitOfWork.FileStorageRepository.GetInfo(path).Length;
            }

            file.AccessId = model.AccessId;
            file.Description = model.Description;
            file.Title = model.Title;

            var newFile = await _unitOfWork.FileRepository.UpdateAsync(file);
            await _unitOfWork.SaveChangesAsync();
            return newFile;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var file = await _unitOfWork.FileRepository.GetByIdAsync(id);
             _unitOfWork.FileStorageRepository.Delete(file.Url);
             await _unitOfWork.FileRepository.DeleteByIdAsync(id);
             await _unitOfWork.SaveChangesAsync();
        }

        public async Task<byte[]> ReadFileAsync(FileDto model)
        {
            var file = await _unitOfWork.FileRepository.GetByIdAsync(model.Id);
            return await _unitOfWork.FileStorageRepository.ReadAsync(file.Url);
        }

        public IEnumerable<FileDto> GetByKeyword(string keyword)
        {
            var list = _unitOfWork.FileRepository.GetAll().Where(x => x.Title.Contains(keyword)
                || x.Description.Contains(keyword)).ToList();
            return _autoMapper.Map<IEnumerable<FileDto>>(list);
        }
        
        public async Task<IEnumerable<AccessDto>> GetFileAccesses()
        {
            var list = await _unitOfWork.FileRepository.GetAccesses();
            return _autoMapper.Map<IEnumerable<AccessDto>>(list);
        }
        
    }
}