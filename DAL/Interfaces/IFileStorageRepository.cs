using System.IO;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IFileStorageRepository
    {
        Task<string> CreateAsync(Stream dataStream, string filename, string fileExtension);
        Task<byte[]> ReadAsync(string filepath);
        FileInfo GetInfo(string filepath);
        void Delete(string filepath);
    }
}