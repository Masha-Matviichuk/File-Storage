using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PL.Helpers;
using PL.Models;
using PL.Models.Account;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommonController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        private readonly IAccessService _accessService;
        
        public CommonController(IAccessService accessService, IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _accessService = accessService;
            _mapper = mapper;
        }
        // GET api/Common/getRole
        [HttpGet("getRole")]
        public async Task<IActionResult> GetRole()
        {
            var user = User.Identity?.Name;
            if (user == null) return BadRequest();
            return Ok(await _roleService.GetRole(user));
        } 
        
        // GET api/Common/getRoles
        [HttpGet("getRoles")]
        public  IActionResult GetRoles()
        {
            return Ok( _roleService.GetRoles());
        } 
        
        // POST api/Common/createRole
        [HttpPost("createRole")]
        public async Task<IActionResult> CreateRole(CreateRoleModel model)
        {
            await _roleService.CreateRole(model.RoleName);
            return Ok();
        }
        
        // GET api/Common/accessList
        [HttpGet("accessList")]
        public async Task<IActionResult> GetFilesAccesses()
        {
            var files =_mapper.Map<IEnumerable<AccessListModel>>(await _accessService.GetFileAccesses());

            return Ok(files);
        }
    }
}