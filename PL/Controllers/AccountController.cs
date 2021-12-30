﻿using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models.Account;
using DAL.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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

        public AccountController(IMapper mapper, IAccountService accountService, IOptionsSnapshot<JwtSettings> optionsSnapshot)
        {
            _mapper = mapper;
            _accountService = accountService;
            _optionsSnapshot = optionsSnapshot;
        }
        
        // POST api/Account/signUp
        [HttpPost("signUp")]
        public async Task<IActionResult> Register([FromForm]SignUpModel model)
        {
            if (model.Password!=model.PasswordConfirm)
            {
                return BadRequest(ModelState);
            }
            var userInfo = _mapper.Map<SignUp>(model);
            
            await _accountService.SignUp(userInfo);

            return Created(string.Empty, string.Empty);
        }
        
        // POST api/Account/logIn
        [HttpPost("logIn")]
        public async Task<IActionResult> LogIn(LogInModel model)
        {
            var userData = _mapper.Map<LogIn>(model);
            var user = await _accountService.LogIn(userData);

            if (user is null) return BadRequest();

            var roles = await _accountService.GetRoles(user);

            return Ok(JwtHelper.GenerateJwt(user, roles, _optionsSnapshot.Value));
        }
        
        // POST api/Account/Logout
        /*[HttpPost("LogOut")]
        public async Task<IActionResult> Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }*/
        
        //it`s another controller maybe
        
    }
}