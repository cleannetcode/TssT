using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TssT.Core.Models;

namespace TssT.Core.Interfaces
{
    public interface IUsersManagerService
    {
        Task<IdentityResult> Create(Core.Models.User newUser);
        Task<IdentityResult> Update(Core.Models.User user);
        Task<IdentityResult> DeleteById(string userId);
        Task<IdentityResult> DeleteByUserName(string userName);
        Task<Core.Models.User> GetById(string userId);
        Task<User> GetByUserName(string username);
    }
}