using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models.Account;
using DAL.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PL.Helpers;
using PL.Models.Account;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IOptionsSnapshot<JwtSettings> _optionsSnapshot;
        private readonly IRoleService _roleService;

        public AccountController(IMapper mapper, IAccountService accountService,
            IOptionsSnapshot<JwtSettings> optionsSnapshot, IRoleService roleService)
        {
            _mapper = mapper;
            _accountService = accountService;
            _optionsSnapshot = optionsSnapshot;
            _roleService = roleService;
        }

        // POST api/Account/signUp
        [HttpPost("signUp")]
        public async Task<IActionResult> Register([FromBody] SignUpModel model)
        {
            if (model.Password != model.PasswordConfirm)
            {
                return BadRequest(ModelState);
            }

            var userInfo = _mapper.Map<SignUp>(model);

            await _accountService.SignUp(userInfo);
            //ne testula
            var rolelist = new AssignUserToRoles() {Email = userInfo.Email, Roles = new string[]{"user"}};
            await _roleService.AssignUserToRoles(rolelist);

            return Created(string.Empty, string.Empty);
        }

        // POST api/Account/logIn
        [HttpPost("logIn")]
        public async Task<IActionResult> LogIn( LogInModel model)
        {
            var userData = _mapper.Map<LogIn>(model);
            var user =  await _accountService.LogIn(userData);

            if (user is null) return BadRequest();

            var roles = await _roleService.GetRoles(user);
            var access_token = JwtHelper.GenerateJwt(user, roles, _optionsSnapshot.Value); 
            //Dictionary<string, string> ob = new Dictionary<string, string>();
            //ob.Add("access_token", access_token);
            //JsonConvert.SerializeObject(
            return Ok(access_token) ;
        }
        
        // POST api/Account/Logout
        /*[HttpPost("LogOut")]
        public async Task<IActionResult> Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }*/
        
        //it`s another controller maybe
        // GET api/Account/getRole
        
        
        /*[HttpDelete("delete")]
        public async Task<IActionResult> DeleteAccount()
        {
            var u = User.Identity.IsAuthenticated;
            Console.WriteLine(u);
            var user = User.Identity.Name;
            return Ok(await _roleService.GetRole(user));
        }  */
        
        [HttpGet("getRole")]
        public async Task<IActionResult> GetRole()
        {
            var u = User.Identity.IsAuthenticated;
            Console.WriteLine(u);
            var user = User.Identity.Name;
            return Ok(await _roleService.GetRole(user));
        } 
        
        // GET api/Account/getRoles
        [HttpGet("getRoles")]
        public  IActionResult GetRoles()
        {
            return Ok( _roleService.GetRoles());
        } 
    }
}