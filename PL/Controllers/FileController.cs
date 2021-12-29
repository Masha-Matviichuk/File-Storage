using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Models.Account;
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
    public class FileController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
      

        public FileController(IMapper mapper, IFileService filetService)
        {
            _mapper = mapper;
            _fileService = filetService;
        }
        
        [HttpGet("files")]
        public async Task<IActionResult> GetAllFiles()
        {
            var files = await _fileService.GetAllAsync();

            return Ok(files);
        }
        
        [HttpGet("file/{id:int}")]
        public async Task<IActionResult> GetFileById(int id)
        {
            var file = await _fileService.GetByIdAsync(id);

            return Ok(file);
        }
        
        [HttpGet("filesSearch/{keyword}")]
        public IActionResult GetFilesByKeyword(string keyword)
        {
            var files = _fileService.GetByKeyword(keyword);

            return Ok(files);
        }
        
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] CreateFileModel model)
        {
            var fileInfo = _mapper.Map<FileDto>(model);
            var result = await Request.ReadFormAsync();
            var file = result.Files[0].OpenReadStream();
            var createdFile = await _fileService.AddAsync(file, fileInfo);
           
            return Created(fileInfo.Title, createdFile.Url);
        }
        
        [HttpPost("download/{id:int}")]
        public async Task<IActionResult> DownloadFile( int id)
        {

            var file = await _fileService.GetByIdAsync(id);
            if (file == null) return NotFound();
            
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(file.Url, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            
            var data = await _fileService.ReadFileAsync(file);
            
            return File(data, contentType, Path.GetFileName(file.Url));
           
        }
        
        [HttpPut("edit")]
        public async Task<IActionResult> EditFile( CreateFileModel model)
        {

            var fileInfo = _mapper.Map<FileDto>(model);
            var result = await Request.ReadFormAsync();
            var file = result.Files[0].OpenReadStream();
            
            
            var data = await _fileService.UpdateAsync(file, fileInfo);

            return Ok(data);

        }
        
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteFile( int id)
        {
            await _fileService.DeleteByIdAsync(id);
            return Ok();
        }
        
    }
}