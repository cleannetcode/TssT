using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TssT.Core.Interfaces
{
    public interface IUsersManagerService
    {
        Task<IdentityResult> Create(Core.Models.User newUser);
        Task<IActionResult> Update(Core.Models.User user);
        Task<IActionResult> Delete(Core.Models.User user);
        Task<Core.Models.User> GetById(int userId);
    }
}