using System.Collections.Generic;
using System.Threading.Tasks;

namespace TssT.Core.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<List<string>> GetUserRoles(string userId);
        Task<bool> AddRoleToUser(string userId, string roleId);
        Task<bool> RemoveRoleFromUser(string userId, string roleId);
    }
}
