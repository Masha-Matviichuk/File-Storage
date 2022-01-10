using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class FileAccessRepository : IFileAccessRepository
    {
        private readonly FileStorageDBContext _context;
        
        public FileAccessRepository(FileStorageDBContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Access>> GetAccesses()
        {
            return await _context.Accesses.ToListAsync();
        }
    }
}