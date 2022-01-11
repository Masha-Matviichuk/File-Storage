using System.Collections.Generic;
using System.Threading.Tasks;
using Auth;
using Auth.Entities;
using BLL.Models.Account;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Interfaces
{
    public interface IAccountService
    {
        Task SignUp(SignUpDto data);
        Task<UserProfile> LogIn(LogInDto data);
        Task DeleteAccount(int id);
       
    }
}