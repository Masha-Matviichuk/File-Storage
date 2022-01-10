using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Auth;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using PL.Helpers;
using PL.Models;
using PL.Models.Account;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class FilesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;


        public FilesController(IMapper mapper, IFileService filetService, IUserService userService)
        {
            _mapper = mapper;
            _fileService = filetService;
            _userService = userService;
        }
        
        // GET api/Files
        [Authorize (Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllFiles()
        {
            var files = await _fileService.GetAllAsync();

            return Ok(files);
        }
        
        // GET api/Files/5
        [Authorize (Roles = "admin, user")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFileById(int id)
        {
           
            var user = User.Identity?.Name;
             if (user == null) throw new NullReferenceException(nameof(user));

             /*if (await _userService.CheckBan(user, DateTime.Now))
            {
                return BadRequest("User is banned!");
            }*/
            var file = await _fileService.GetByIdAsync(id, user);
            if (file.Id == 0)
            {
                return BadRequest();
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
            
            if (await _userService.CheckBan(userEmail, DateTime.Now))
            {
                return BadRequest("User is banned!");
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
            if (await _userService.CheckBan(user, DateTime.Now))
            {
                return BadRequest("User is banned!");
            }
            
            var file = uploadedFile.OpenReadStream();
            var fileInfo = _mapper.Map<FileDto>(model);
            //fileInfo.Title = Path.GetFileName(uploadedFile.FileName);
            fileInfo.Extension = Path.GetExtension(uploadedFile.FileName);
            fileInfo.CurrentUser = User;
            /*var result = await Request.ReadFormAsync();
            var file = result.Files[0].OpenReadStream();*/
            var createdFile = await _fileService.AddAsync(file, fileInfo);

            return Created(fileInfo.Title, createdFile.Url);
        }
        
        // GET api/Files/download/4
        [HttpGet("download/{id:int}")]
        public async Task<IActionResult> DownloadFile( int id)
        {

            var user = User.Identity?.Name;
            if (await _userService.CheckBan(user, DateTime.Now))
            {
                return BadRequest("User is banned!");
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
          //Path.GetFileName(file.Url)
        }
        
        //!!!!!!!!!!!!!!!!!!!!!!!!
        // PUT api/Files/edit
        [Authorize (Roles = "admin, user")]
        [HttpPut("edit/{id:int}")]
        [FileUploadOperation.FileContentType]
        public async Task<IActionResult> EditFile( [FromForm] CreateFileModel model, IFormFile uploadedFile, int id)
        {
            var user = User.Identity?.Name;
            if (await _userService.CheckBan(user, DateTime.Now))
            {
                return BadRequest("User is banned!");
            }
            
                var file = uploadedFile.OpenReadStream();


                /*var oldFile = await _fileService.GetByIdAsync(id, User.Identity.Name);
                if (oldFile == null) return NotFound();
                
                var provider = new FileExtensionContentTypeProvider();
                
                if (!provider.TryGetContentType(file.Url, out var contentType))
                {
                    contentType = "application/octet-stream";
                }
                
                var olddata = await _fileService.ReadFileAsync(oldFile);
                var oldFileS = new FileStream(oldFile.Url, FileAccess.Read);*/

            var fileInfo = _mapper.Map<FileDto>(model);
            fileInfo.Id = id;
            fileInfo.Extension = Path.GetExtension(uploadedFile.FileName);
            //fileInfo.Title = uploadedFile.FileName;
            /*var result = await Request.ReadFormAsync();
            var file = result.Files[0].OpenReadStream();*/
            
            
            var data = await _fileService.UpdateAsync(file, fileInfo);

            return Ok(data);

        }
        
        // DELETE api/Files/delete/5
        [Authorize(Roles = "user, admin")]
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var user = User.Identity?.Name;
            if (await _userService.CheckBan(user, DateTime.Now))
            {
                return BadRequest("User is banned!");
            }
            await _fileService.DeleteByIdAsync(id);
            return Ok();
        }
        
        
        [HttpGet("accessList")]
        public async Task<IActionResult> GetFilesAccesses()
        {
            var files =_mapper.Map<IEnumerable<AccessListModel>>(await _fileService.GetFileAccesses());

            return Ok(files);
        }
        /*
        // GET api/Admin/users/4/files/2
        [HttpGet("{userId:int}/files/{fileId:int}")]
        public async Task<IActionResult> GetUserFilesById(int userId, int fileId)
        {
            var files = await _userService.GetAllUsersFiles(userId);
            var file = files.FirstOrDefault(f => f.Id == fileId);

            return Ok(file);
        }
        */
        
        // GET api/Files/users/4/files
        [Authorize(Roles = "user, admin")]
        [HttpGet("userFiles")]
        public async Task<IActionResult> GetUserFiles()
        {
            var user = User.Identity.Name;
            var files = await _fileService.GetAllUsersFiles(user);

            return Ok(files);
        }

    }
}