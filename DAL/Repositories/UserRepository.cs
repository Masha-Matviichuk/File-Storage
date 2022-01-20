using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly FileStorageDBContext _context;

        public UserRepository(FileStorageDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> CreateAsync(User entity)
        {
           var user = await _context.Users.AddAsync(entity);
           await _context.SaveChangesAsync();
           return user.Entity;
        }

        public async Task<User> UpdateAsync(User entity)
        {
             var newUser =_context.Users.Attach(entity);
             await _context.SaveChangesAsync();
             return newUser.Entity;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity= await _context.Users.FindAsync(id);
            if (entity == null) return;
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}