
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Models;


namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserInfoDto>> GetAllAsync();
        Task<UserInfoDto> GetByIdAsync(int id);
        Task<bool> CheckBan(string userEmail);
        Task UserBan(int id, int days);
    }
}