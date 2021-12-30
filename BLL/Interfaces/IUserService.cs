using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IUserService : ICrud<UserInfoDto>
    {
       // Task AddAsync(UserInfoDto model);
        Task UpdateAsync(UserInfoDto model);
        Task DeleteByIdAsync(int id);
        Task<IEnumerable<FileDto>> GetAllUsersFiles(int userId);

        /*
        Task<FileDto> GetUserFileById(int userId, int fileId);
        */


        //Maybe I add ban, getting all users files...
    }
}