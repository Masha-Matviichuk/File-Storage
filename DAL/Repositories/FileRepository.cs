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
            public IQueryable<File> GetAll()
            {
                return _context.Files;
            }

        public async Task<IEnumerable<File>> GetAllAsync()
        {
            return await _context.Files.ToListAsync();
        }

        public File GetById(int id)
        {
            return _context.Files.Find(id);
        }

        public async Task<File> GetByIdAsync(int id)
        {
            var file = await _context.Files.FindAsync(id);
                file.Access = await _context.Accesses.FindAsync(file.AccessId);
                return file;
        }

        public void Create(File entity)
        { 
            _context.Files.Add(entity);
            _context.SaveChanges();
        }

        public async Task<File> CreateAsync(File entity)
        { 
           var file = await _context.Files.AddAsync(entity);
            await _context.SaveChangesAsync();
            return file.Entity;
        }

        public void Update(File entity)
        {
           var newFile = _context.Files.Attach(entity);
            _context.SaveChanges();
        }

        public async Task<File> UpdateAsync(File entity)
        {
             var newFile =_context.Files.Attach(entity);
             await _context.SaveChangesAsync();
             return newFile.Entity;
        }

        public void Delete(int id)
        {
            var entity = _context.Files.Find(id);
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _context.Files.FindAsync(id);
            _context.Files.Remove(entity);
            await _context.SaveChangesAsync();
        }
        //maybe another
        public async Task<IEnumerable<Access>> GetAccesses()
        {
            return await _context.Accesses.ToListAsync();
        }
    }
}