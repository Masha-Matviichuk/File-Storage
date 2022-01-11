using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IFileAccessRepository
    {
        Task<IEnumerable<Access>> GetAll();
    }
}