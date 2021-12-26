using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FileStorageDBContext _context;

        public UserRepository(FileStorageDBContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public void Create(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public async Task CreateAsync(User entity)
        {
           await _context.Users.AddAsync(entity);
           await _context.SaveChangesAsync();
        }

        public void Update(User entity)
        {
            _context.Users.Attach(entity);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(User entity)
        {
             _context.Users.Attach(entity);
             await _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var entity= _context.Users.Find(id);
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity= await _context.Users.FindAsync(id);
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}