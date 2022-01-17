
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models.Account;
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
            
            if (model.Password != model.PasswordConfirm || !model.Email.Contains('@'))
            {
                return BadRequest(ModelState);
            }

            var userInfo = _mapper.Map<SignUpDto>(model);

            await _accountService.SignUp(userInfo);
            var rolelist = new AssignUserToRolesDto() {Email = userInfo.Email, Roles = new string[]{"user"}};
            await _roleService.AssignUserToRoles(rolelist);

            return Created(string.Empty, string.Empty);
        }

        // POST api/Account/logIn
        [HttpPost("logIn")]
        public async Task<IActionResult> LogIn( LogInModel model)
        {
            var userData = _mapper.Map<LogInDto>(model);
            var user =  await _accountService.LogIn(userData);

            if (user is null) return BadRequest();

            var roles = await _roleService.GetRoles(user);
            var accessToken = JwtHelper.GenerateJwt(user, roles, _optionsSnapshot.Value);
            return Ok(accessToken) ;
        }
    }
}