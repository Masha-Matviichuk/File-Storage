using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class FileStorageRepository : IFileStorageRepository
    {
        private static readonly int BufferSize = 4096;

        public string Create(Stream dataStream, string filename)
        {
            var filePath = FullPath(filename);
            using (var fileStream = File.OpenWrite(filePath))
            {
                using (var data = new BinaryReader(dataStream))
                {
                    byte[] bytes;
                    do
                    {
                        bytes = data.ReadBytes(BufferSize);
                        fileStream.Write(bytes, 0, bytes.Length);
                    } while (bytes.Length >= BufferSize);
                }
            }

            return filePath;
        }

        public async Task<string> CreateAsync(Stream dataStream, string filename)
        {
            var filePath = FullPath(filename);
            await Task.Run(() =>
            {
                using (var fileStream = File.OpenWrite(filePath))
                {
                    using (var data = new BinaryReader(dataStream))
                    {
                        byte[] bytes;
                        do
                        {
                            bytes = data.ReadBytes(BufferSize);
                            fileStream.Write(bytes, 0, bytes.Length);
                        } while (bytes.Length >= BufferSize);
                    }
                }
            });

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

        private string FullPath(string filename)
        {
            var normalizedFileName = GetAppropriateFileName(filename);

            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "FileStorageAppData");

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            return Path.Combine(
                path,
                normalizedFileName);
        }

        private string GetAppropriateFileName(string filename)
        {
            var filenameChars =
                filename
                    .Trim()
                    .Replace(' ', '-')
                    .ToLowerInvariant()
                    .ToArray();

            return new string(filenameChars);
        }
    }
}