using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly FileStorageDBContext _context;

        public FileRepository(FileStorageDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<File>> GetAllAsync()
        {
            return await _context.Files.ToListAsync();
        }

        public async Task<File> GetByIdAsync(int id)
        {
            var file = await _context.Files.FindAsync(id);
                file.Access = await _context.Accesses.FindAsync(file.AccessId);
                return file;
        }
        
        public async Task<File> CreateAsync(File entity)
        { 
           var file = await _context.Files.AddAsync(entity);
            await _context.SaveChangesAsync();
            return file.Entity;
        }

        public async Task<File> UpdateAsync(File entity)
        {
             var newFile =_context.Files.Attach(entity);
             await _context.SaveChangesAsync();
             return newFile.Entity;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _context.Files.FindAsync(id);
            if (entity == null) return;
            _context.Files.Remove(entity);
            await _context.SaveChangesAsync();
        }
        
    }
}