using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TssT.Core.Interfaces
{
    public interface IRoleService
    {
        Task<Core.Models.Role[]> GetAll();
        Task<bool> Update(string roleId, string newRoleName);
        Task<bool> Delete(string roleId);
        Task<Core.Models.Role> Create(string roleName);
    }
}
