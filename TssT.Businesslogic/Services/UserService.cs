using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TssT.Core.Interfaces;
using TssT.Core.Models;

namespace TssT.Businesslogic.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _usersRepository;

        public UserService(IUserRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        /// <summary>
        /// Создать пользователя.
        /// </summary>
        /// <param name="userForCreate">Пользователь.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Созданный пользователь.</returns>
        public Task<User> Create(User userForCreate, string password)
        {
            return _usersRepository.Create(userForCreate, password);
        }

        /// <summary>
        /// Удалить пользователя по идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>true - успешно удалён, false - не удалён.</returns>
        public Task<bool> DeleteById(string userId)
        {
            return _usersRepository.DeleteById(userId);
        }

        /// <summary>
        /// Получить список всех пользователей.
        /// </summary>
        /// <returns></returns>
        public Task<User[]> GetAll()
        {
            return _usersRepository.GetAll();
        }

        /// <summary>
        /// Проверяем является ли пользователь аутентифицированным (логин принадлежит владельцу (по паролю)).
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="userPassword">Пароль пользователя.</param>
        /// <returns></returns>
        public async Task<User> GetByNameAndPassword(string userName, string userPassword)
        {
            var user = await _usersRepository.GetByNameAndPassword(userName, userPassword);

            return user;
        }
    }
}
