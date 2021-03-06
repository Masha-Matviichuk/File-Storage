
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Models.Account;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]/users")]
    [Authorize (Roles = "admin")]
    public class AdminController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;
      

        public AdminController(IMapper mapper, IUserService userService, 
            IAccountService accountService, IRoleService roleService)
        {
            _mapper = mapper;
            _userService = userService;
            _accountService = accountService;
            _roleService = roleService;
        }
        
        // GET api/Admin/users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();

            return Ok(users);
        }
        
        // GET api/Admin/users/4
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var userEmail = (await _userService.GetAllAsync()).FirstOrDefault(u=>u.Id==id).Email;
            await _userService.CheckBan(userEmail);
            var user = await _userService.GetByIdAsync(id);

            return Ok(user);
        }
        

        // POST api/Admin/assignUserToRole
        [HttpPost("assignUserToRole")]
        public async Task<IActionResult> AssignUserToRole(AssignUserToRolesModel model)
        {
            await _roleService.AssignUserToRoles(new AssignUserToRolesDto
            {
                Email = model.Email,
                Roles = model.Roles,

            });

            return Ok();
        }

        
        // PUT api/Admin/ban/5
        [HttpPut("ban/{id:int}")]
        public async Task<IActionResult> BlockUser(int id, int days)
        {
            await _userService.UserBan(id, days);

            return Ok();
        }
        
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            await _accountService.DeleteAccount(id);
            return Ok();
        }  
        
    }
}