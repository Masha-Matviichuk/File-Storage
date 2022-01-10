using System.Collections.Generic;
using System.Threading.Tasks;
using Auth;
using BLL.Models.Account;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Interfaces
{
    public interface IAccountService
    {
        Task SignUp(SignUp data);
        Task<UserProfile> LogIn(LogIn data);
        Task DeleteAccount(int id);
       
    }
}