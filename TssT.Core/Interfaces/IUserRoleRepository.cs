using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TssT.Core.Models;

namespace TssT.Core.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<List<string>> GetUserRoles(User user);
        Task<bool> AddRoleToUser(string userId, string roleId);
    }
}
