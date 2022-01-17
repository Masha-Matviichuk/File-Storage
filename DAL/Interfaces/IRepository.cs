using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
      
        Task<IEnumerable<TEntity>> GetAllAsync();
       
        Task<TEntity> GetByIdAsync(int id);
      
        Task<TEntity> CreateAsync(TEntity entity);
      
        Task<TEntity> UpdateAsync(TEntity entity);
      
        Task DeleteByIdAsync(int id);
    }
}