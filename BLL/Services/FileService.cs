using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _autoMapper;
        

        public FileService(IUnitOfWork unitOfWork, Mapper autoMapper)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
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


        public async Task AddAsync(Stream fileStream, FileDto model)
        {
            var path = await _unitOfWork.FileStorageRepository.CreateAsync(fileStream, model.Title);

            var file = _autoMapper.Map<DAL.Entities.File>(model);
            file.Url = path;
            file.Size = _unitOfWork.FileStorageRepository.GetInfo(path).Length;
            file.Upload = DateTime.Now;

            await _unitOfWork.FileRepository.CreateAsync(file);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(Stream fileStream, FileDto model)
        {
            var file = await _unitOfWork.FileRepository.GetByIdAsync(model.Id);
            if (fileStream != null)
            {
                _unitOfWork.FileStorageRepository.Delete(file.Url);
                 var path = await _unitOfWork.FileStorageRepository.CreateAsync(fileStream, model.Title);
                 file.Url = path;
                 file.Size = _unitOfWork.FileStorageRepository.GetInfo(path).Length;
            }

            file.AccessId = model.AccessId;
            file.Description = model.Description;
            file.Title = model.Title;

            await _unitOfWork.FileRepository.UpdateAsync(file);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var file = await _unitOfWork.FileRepository.GetByIdAsync(id);
             _unitOfWork.FileStorageRepository.Delete(file.Url);
             await _unitOfWork.FileRepository.DeleteByIdAsync(id);
             await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Stream> ReadFileAsync(FileDto model)
        {
            var file = await _unitOfWork.FileRepository.GetByIdAsync(model.Id);
            return _unitOfWork.FileStorageRepository.Read(file.Url);
        }

        public IEnumerable<FileDto> GetByKeyword(string keyword)
        {
            var list = _unitOfWork.FileRepository.GetAll().Where(x => x.Title.Contains(keyword)
                || x.Description.Contains(keyword)).ToList();
            return _autoMapper.Map<IEnumerable<FileDto>>(list);
        }
    }
}