using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IAccessService
    {
        Task<IEnumerable<AccessDto>> GetFileAccesses();
    }
}