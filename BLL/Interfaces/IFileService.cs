using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IFileService:ICrud<FileDto>
    {
        Task AddAsync(Stream fileStream,  FileDto model);
        Task UpdateAsync(Stream fileStream,  FileDto model);
        Task DeleteByIdAsync(int id);
        Task<Stream> ReadFileAsync(FileDto model);
        IEnumerable<FileDto> GetByKeyword(string keyword);
    }
}