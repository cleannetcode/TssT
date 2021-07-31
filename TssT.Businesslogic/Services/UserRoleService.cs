using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TssT.Core.Interfaces;
using TssT.Core.Models;

namespace TssT.Businesslogic.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<List<string>> GetUserRoles(string userId)
        {
            return await _userRoleRepository.GetUserRoles(userId);
        }

        public Task<bool> AddRoleToUser(string userId, string roleId)
        {
            return _userRoleRepository.AddRoleToUser(userId, roleId);
        }

        public Task<bool> RemoveRoleFromUser(string userId, string roleId)
        {
            return _userRoleRepository.RemoveRoleFromUser(userId, roleId);
        }
    }
}
