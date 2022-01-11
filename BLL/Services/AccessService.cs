using System.Collections.Generic;
using System.Threading.Tasks;
using Auth.Entities;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class AccessService : IAccessService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;


        public AccessService(IUnitOfWork unitOfWork, IMapper autoMapper)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
        }
        
        public async Task<IEnumerable<AccessDto>> GetFileAccesses()
        {
            var list = await _unitOfWork.FileAccessRepository.GetAll();
            return _autoMapper.Map<IEnumerable<AccessDto>>(list);
        }
    }
}