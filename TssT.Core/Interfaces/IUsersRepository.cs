using System.Threading.Tasks;
using TssT.Core.Models;

namespace TssT.Core.Interfaces
{
    public interface IUsersRepository
    {
        void Create(User newUser);
        void Update(User user);
        void Delete(User user);
        User GetById(int userId);
        Task<User> GetByNameAndPassword(string userName, string userPassword);
    }
}