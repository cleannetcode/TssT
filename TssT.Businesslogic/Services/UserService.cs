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
