using System.Threading.Tasks;

namespace TssT.Core.Interfaces
{
    public interface IRoleRepository
    {
        Task<Core.Models.Role[]> GetAll();
        Task<bool> Update(string roleId, string newRoleName);
        Task<bool> Delete(string roleId);
        Task<Core.Models.Role> Create(string roleName);
    }
}
