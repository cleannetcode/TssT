using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TssT.Core.Models;

namespace TssT.DataAccess.Repositories
{
    public interface IUsersRepository
    {
        Task<IdentityResult> Create(Core.Models.User newUser);
        Task<IdentityResult> Update(User user);
        Task<IdentityResult> Delete(Core.Models.User user);
        Task<User> GetById(string userId);
        Task<User> GetByUserName(string username);
    }
}