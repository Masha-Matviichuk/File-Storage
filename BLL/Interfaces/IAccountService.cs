
using System.Threading.Tasks;
using Auth.Entities;
using BLL.Models.Account;


namespace BLL.Interfaces
{
    public interface IAccountService
    {
        Task SignUp(SignUpDto data);
        Task<UserProfile> LogIn(LogInDto data);
        Task DeleteAccount(int id);

    }
}