using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IUserService : ICrud<UserInfoDto>
    {
       // Task AddAsync(UserInfoDto model);
        Task UpdateAsync(UserInfoDto model);
        Task DeleteByIdAsync(int id);
        
        //Maybe I add ban, getting all users files...
    }
}