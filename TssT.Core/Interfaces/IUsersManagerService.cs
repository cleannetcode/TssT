namespace TssT.Core.Interfaces
{
    public interface IUsersManagerService
    {
        void Create(Core.Models.User newUser);
        void Update(Core.Models.User user);
        void Delete(Core.Models.User user);
        Core.Models.User GetById(int userId);
    }
}