using System.Threading.Tasks;
using TssT.Core.Interfaces;
using TssT.Core.Models;

namespace TssT.Businesslogic.Services
{
    public class RoleService: IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Task<Role> Create(string roleName)
        {
            return _roleRepository.Create(roleName);
        }

        public Task<bool> Delete(string roleId)
        {
            return _roleRepository.Delete(roleId);
        }

        public Task<Role[]> GetAll()
        {
            return _roleRepository.GetAll();
        }

        public Task<bool> Update(string roleId, string newRoleName)
        {
            return _roleRepository.Update(roleId, newRoleName);
        }
    }
}
