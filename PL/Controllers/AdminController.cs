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
      

        public AdminController(IMapper mapper, IUserService userService, IAccountService accountService)
        {
            _mapper = mapper;
            _userService = userService;
            _accountService = accountService;
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
            var user = await _userService.GetByIdAsync(id);

            return Ok(user);
        }
        
        // GET api/Admin/users/4/files
        [HttpGet("{userId:int}/files")]
        public async Task<IActionResult> GetUserFiles(int userId)
        {
            var files = await _userService.GetAllUsersFiles(userId);

            return Ok(files);
        }
        
        // GET api/Admin/users/4/files/2
        [HttpGet("{userId:int}/files/{fileId:int}")]
        public async Task<IActionResult> GetUserFileById(int userId, int fileId)
        {
            var files = await _userService.GetAllUsersFiles(userId);
            var file = files.FirstOrDefault(f => f.Id == fileId);

            return Ok(file);
        }
        
        // POST api/Admin/createRole
        [HttpPost("createRole")]
        public async Task<IActionResult> CreateRole(CreateRoleModel model)
        {
            await _accountService.CreateRole(model.RoleName);
            return Ok();
        }

        // GET api/Admin/getRole
        [HttpGet("getRoles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _accountService.GetRoles());
        }

        // POST api/Admin/assignUserToRole
        [HttpPost("assignUserToRole")]
        public async Task<IActionResult> AssignUserToRole(AssignUserToRolesModel model)
        {
            await _accountService.AssignUserToRoles(new AssignUserToRoles
            {
                Email = model.Email,
                Roles = model.Roles,

            });

            return Ok();
        }

        
        //Add ban for users
        
        
    }
}