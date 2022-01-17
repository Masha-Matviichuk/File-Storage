
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using PL.Helpers;
using PL.Models;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class FilesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;
       


        public FilesController(IMapper mapper, IFileService fileService, IUserService userService)
        {
            _mapper = mapper;
            _fileService = fileService;
            _userService = userService;
        }

        // GET api/Files/5
        //[Authorize (Roles = "admin, user")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFileById(int id)
        {
           
            var user = User.Identity?.Name;

            var file = await _fileService.GetByIdAsync(id, user);
            if (file.Id == 0)
            {
                return Forbid();
            }
            var fileModel = _mapper.Map<FileInfoModel>(file);

            return Ok(fileModel);
        }
        
        // GET api/Files/fileSearch/word
        [Authorize (Roles = "admin, user")]
        [HttpGet("filesSearch/{keyword}")]
        public async Task<IActionResult> GetFilesByKeyword(string keyword)
        {
           
            
            var userEmail = User.Identity?.Name;
            
            if (userEmail==null)
            {
                return BadRequest();
            }
            
            if (await _userService.CheckBan(userEmail))
            {
                return Forbid();
            }
            var files = await _fileService.GetByKeyword(keyword, userEmail);

            return Ok(files);
        }
        
        
        // POST api/Files/upload
        [Authorize(Roles = "user, admin")]
        [HttpPost("upload")]
        [FileUploadOperation.FileContentType]
        public async Task<IActionResult> UploadFile([FromForm] CreateFileModel model, IFormFile uploadedFile)
        {
            
            var user = User.Identity?.Name;
            if (user == null) return NotFound();
                if (await _userService.CheckBan(user))
            {
                return Forbid();
            }
            
            var file = uploadedFile.OpenReadStream();
            var fileInfo = _mapper.Map<FileDto>(model);
            fileInfo.Extension = Path.GetExtension(uploadedFile.FileName);
            fileInfo.CurrentUser = User;
            var createdFile = await _fileService.AddAsync(file, fileInfo);
           
            return Created(fileInfo.Title, createdFile.Url);
           
        }
        
        // GET api/Files/download/4
        [HttpGet("download/{id:int}")]
        public async Task<IActionResult> DownloadFile( int id)
        {

            var user = User.Identity?.Name;
            
            if (await _userService.CheckBan(user))
            {
                return Forbid();
            }
            var file = await _fileService.GetByIdAsync(id, user);
            if (file == null) return NotFound();
            
            var provider = new FileExtensionContentTypeProvider();
            
            if (!provider.TryGetContentType(file.Url, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            
            var data = await _fileService.ReadFileAsync(file);

            return File(data, contentType, file.Title);
          
        }
        
        
        // PUT api/Files/edit/9
        [Authorize (Roles = "admin, user")]
        [HttpPut("edit/{id:int}")]
        [FileUploadOperation.FileContentType]
        public async Task<IActionResult> EditFile( [FromForm] CreateFileModel model, IFormFile uploadedFile, int id)
        {
            var user = User.Identity?.Name;
            
            if (user==null)
            {
                return BadRequest();
            }
            
            if (await _userService.CheckBan(user))
            {
                return Forbid();
            }
            
            var file = uploadedFile.OpenReadStream();
                
            var fileInfo = _mapper.Map<FileDto>(model);
            fileInfo.Id = id;
            fileInfo.Extension = Path.GetExtension(uploadedFile.FileName);

            var data = await _fileService.UpdateAsync(file, fileInfo);

            return Ok(data);

        }
        
        // DELETE api/Files/delete/5
        [Authorize(Roles = "user, admin")]
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var user = User.Identity?.Name;
            
            if (user==null)
            {
                return BadRequest();
            }
            
            if (await _userService.CheckBan(user))
            {
                return Forbid();
            }
            await _fileService.DeleteByIdAsync(id);
            return Ok();
        }

        // GET api/Files
        [Authorize(Roles = "user, admin")]
        [HttpGet]
        public async Task<IActionResult> GetUserFiles()
        {
            var user = User.Identity?.Name;
            if (user==null)
            {
                return BadRequest();
            }
            var files = await _fileService.GetAllFilesAsync(user);

            return Ok(files);
        }

    }
}