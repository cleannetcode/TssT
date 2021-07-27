using System.Collections.Generic;
using System.Threading.Tasks;
using TssT.Core.Models;

namespace TssT.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Create(User newUser, string password);
        Task<bool> Update(User user);
        Task<bool> DeleteById(string userId);
        Task<User> GetById(string userId);
        Task<User[]> GetAll();
        Task<User> GetByNameAndPassword(string userName, string userPassword);
    }
}