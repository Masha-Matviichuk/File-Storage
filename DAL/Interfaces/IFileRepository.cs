using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DAL.Entities;
using File = DAL.Entities.File;

namespace DAL.Interfaces
{
    public interface IFileRepository : IRepository<File>
    {
         Task<IEnumerable<Access>> GetAccesses();
    }
}