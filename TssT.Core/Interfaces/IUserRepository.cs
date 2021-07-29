using System.Collections.Generic;
using System.Threading.Tasks;
using TssT.Core.Models;

namespace TssT.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Create(User newUser);
        Task<bool> Update(User user);
        Task<bool> Delete(string userId);
        Task<User> GetById(string userId);
        Task<User> GetByNameAndPassword(string userName, string userPassword);
        Task<List<string>> GetUserRoles(User user);
    }
}