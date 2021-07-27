using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TssT.Core.Interfaces
{
    public interface IUserRoleService
    {
        Task<List<string>> GetUserRoles(string userId);
        Task<bool> AddRoleToUser(string userId, string roleId);
        Task<bool> RemoveRoleFromUser(string userId, string roleId);
    }
}
