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
        
        /// <summary>
        /// This method returns all access types, which database contains
        /// </summary>
        /// <returns>IEnumerable<Access></returns>
        public async Task<IEnumerable<Access>> GetAll()
        {
            return await _context.Accesses.ToListAsync();
        }
    }
}