using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BLL.Models;
using File = DAL.Entities.File;

namespace BLL.Interfaces
{
    public interface IFileService
    {
        Task<File> AddAsync(Stream fileStream,  FileDto model);
        Task<File> UpdateAsync(Stream fileStream,  FileDto model);
        Task DeleteByIdAsync(int id);
        Task<byte[]> ReadFileAsync(FileDto model);
        Task<IEnumerable<FileDto>> GetByKeyword(string keyword,  string userEmail);
       
        Task<FileDto> GetByIdAsync(int id, string userEmail);
        Task<IEnumerable<FileDto>> GetAllFilesAsync(string userEmail);
    }
}