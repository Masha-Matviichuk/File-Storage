using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IUserService : ICrud<UserInfoDto>
    {
        Task UpdateAsync(UserInfoDto model);
        Task<UserInfoDto> GetByIdAsync(int id);
        Task<bool> CheckBan(string userEmail, DateTime presentTime);
        Task UserBan(int id, int days);
    }
}