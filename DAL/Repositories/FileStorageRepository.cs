using System;
using System.IO;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL.Repositories
{
    /// <summary>
    /// This class works with file system.
    /// </summary>
    public class FileStorageRepository : IFileStorageRepository
    {

        public async Task<string> CreateAsync(Stream dataStream, string filename, string fileExtension)
        {
            var filePath = FullPath(fileExtension);
            
            await using (var stream = System.IO.File.Create(filePath))
            {
                await dataStream.CopyToAsync(stream);
                
            }
            return filePath;
        }

        public async Task<byte[]> ReadAsync(string filepath)
        {
            return await File.ReadAllBytesAsync(filepath);
        }

        public FileInfo GetInfo(string filepath)
        {
            var info = new FileInfo(filepath);
            return info;
        }

        public void Delete(string filepath)
        {
            File.Delete(filepath);
        }
/// <summary>
/// This method creates an url, where file will be saved.
/// </summary>
/// <param name="fileExtension"> string extension of uploaded file</param>
/// <returns>A string</returns>
        private string FullPath(string fileExtension)
        {
            var normalizedFileName = Guid.NewGuid().ToString();
            var fullName = string.Concat(normalizedFileName, fileExtension);

            var path = Path.Combine("C:\\Prg",
                "FileStorageAppData");
//Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
//AppDomain.CurrentDomain.BaseDirectory
//"C:\\Prg"
            var dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            return Path.Combine(
                path,
                fullName);
        }
    }
}