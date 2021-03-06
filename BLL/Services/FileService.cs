using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Auth.Entities;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// This method returns info about file by it`s id. Admin can always get info about every file, it`s not depends on access type of this file. User also can sees info about all his files. But if user wanna get info about file of another user and this file is private, then he can`t get access.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<FileDto> GetByIdAsync(int id, string email)
        {
            var entity = await _unitOfWork.FileRepository.GetByIdAsync(id);
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            if (email!=null)
            {
                var users = await _unitOfWork.UserRepository.GetAllAsync(); 
                var userId = users.FirstOrDefault(u => u.Email == email)?.Id;
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
                var userRole = await _userManager.GetRolesAsync(user);
                
                if (userRole.Contains("admin"))
                    return _autoMapper.Map<FileDto>(entity);
                
                if (entity.UserId!=userId && entity.AccessId==1)
                    return new FileDto();
                else
                    return _autoMapper.Map<FileDto>(entity);
            }else
                return entity.AccessId==1 ? new FileDto() : _autoMapper.Map<FileDto>(entity);
        }

     
        public async Task<File> AddAsync(Stream fileStream, FileDto model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (fileStream == null) throw new ArgumentNullException(nameof(fileStream));
            
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
            if (model == null) throw new ArgumentNullException(nameof(model));
            var file = await _unitOfWork.FileRepository.GetByIdAsync(model.Id);
          
            if (fileStream.Length != 0)
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
            if (file == null) return;
             _unitOfWork.FileStorageRepository.Delete(file.Url);
             await _unitOfWork.FileRepository.DeleteByIdAsync(id);
             await _unitOfWork.SaveChangesAsync();
        }

        public async Task<byte[]> ReadFileAsync(FileDto model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            
            var file = await _unitOfWork.FileRepository.GetByIdAsync(model.Id);
            return await _unitOfWork.FileStorageRepository.ReadAsync(file.Url);
        }

        /// <summary>
        /// This method searches all files, where title or description contains keyword.
        /// </summary>
        /// <param name="keyword">A string</param>
        /// <param name="userEmail">A string</param>
        /// <returns>IEnumerable of FileDto</returns>
        /// <exception cref="NullReferenceException"></exception>
        public async  Task<IEnumerable<FileDto>> GetByKeyword(string keyword, string userEmail)
        {
            List<File> list;
            var allFiles = await _unitOfWork.FileRepository.GetAllAsync();
            var user = _userManager.Users.FirstOrDefault(u=>u.UserName==userEmail);

            if (user == null) throw new NullReferenceException();
           
            
            var userId = (await _unitOfWork.UserRepository.GetAllAsync())
                .FirstOrDefault(u => u.Email == user.UserName)?.Id;
            var userFiles = allFiles.Where(f => f.UserId == userId).ToList();
            var roles = (await _userManager.GetRolesAsync(user)).ToList();
            
            if ( roles.Contains("admin"))
                list = allFiles.Where(x => x.Title.Contains(keyword) 
                                           || x.Description.Contains(keyword)).ToList();
            else
                list = userFiles.Where(x => x.Title.Contains(keyword)
                                            || x.Description.Contains(keyword)).ToList();
            
            return _autoMapper.Map<IEnumerable<FileDto>>(list);
        }
        
        /// <summary>
        /// This method returns all files, which user uploaded before for ordinary users. And list of all files in database for admin.
        /// </summary>
        /// <param name="userEmail">A string</param>
        /// <returns>IEnumerable of FileDto </returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<IEnumerable<FileDto>> GetAllFilesAsync(string userEmail)
        {
            var user = _userManager.Users.FirstOrDefault(u=>u.UserName==userEmail);
            if (user == null) throw new NullReferenceException();
            
            var userId = (await _unitOfWork.UserRepository.GetAllAsync())
                .FirstOrDefault(u => u.Email == user.UserName)?.Id;
            
            var allFiles = await _unitOfWork.FileRepository.GetAllAsync();
            var userFiles = allFiles.Where(f => f.UserId == userId).ToList();
            
            
            var roles = (await _userManager.GetRolesAsync(user)).ToList();
            if ( roles.Contains("admin"))
               return _autoMapper.Map<IEnumerable<FileDto>>(allFiles);
            else
                return _autoMapper.Map<IEnumerable<FileDto>>(userFiles);
        }
    }
}