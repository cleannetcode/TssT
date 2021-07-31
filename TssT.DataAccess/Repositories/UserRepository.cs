using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TssT.Core.Interfaces;
using TssT.Core.Models;

namespace TssT.DataAccess.Repositories
{
    /// <summary>
    /// Класс репозиторий для доступа к данным пользователя.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<Entities.User> _userManager;
        private readonly SignInManager<Entities.User> _signInManager;

        public UserRepository(IMapper mapper,
            ApplicationDbContext applicationDbContext,
            UserManager<Entities.User> userManager,
            SignInManager<Entities.User> signInManager
        )
        {
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Создать пользователя.
        /// </summary>
        /// <param name="newUser">Пользователь для создания.</param>
        /// <returns>Созданный пользователь или null.</returns>
        public async Task<User> Create(User newUser, string password)
        {
            Entities.User userForCreate = _mapper.Map<Entities.User>(newUser);
            userForCreate.Id = Guid.NewGuid().ToString();

            var result = await _userManager.CreateAsync(userForCreate, password);

            if (result.Succeeded)
                return _mapper.Map<User>(userForCreate);
            else
                return null;
        }

        /// <summary>
        /// Обновить пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <returns>Результат обновления. true - успешно обновлено, false - обновление не состоялось.</returns>
        public async Task<bool> Update(User user)
        {
            if (user == null)
                throw new ArgumentException(nameof(user));

            var userForUpdate = _mapper.Map<Entities.User>(user);
            var result = await _userManager.UpdateAsync(userForUpdate);
            return result.Succeeded;
        }

        /// <summary>
        /// Удалить пользователя по Id
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <returns>Результат удаления. true - успешно удалён, false - не удалён.</returns>
        public async Task<bool> DeleteById(string userId)
        {
            var userForDelete = await _userManager.FindByIdAsync(userId);
            if (userForDelete == null)
                return false;

            var result = await _userManager.DeleteAsync(userForDelete);
            return result.Succeeded;
        }

        /// <summary>
        /// Получить пользователя по id.
        /// </summary>
        /// <param name="userId">id пользователя.</param>
        /// <returns>Запись пользователя.</returns>
        public async Task<User> GetById(string userId)
        {
            if (String.IsNullOrWhiteSpace(userId))
                throw new ArgumentException(nameof(userId));

            var user = await _userManager
                .Users
                .Where(u => u.Id == userId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return _mapper.Map<User>(user);
        }

        /// <summary>
        /// Получить пользователя по имени и паролю.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Возвращает пользователя.</returns>
        public async Task<User> GetByNameAndPassword(string userName, string userPassword)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException(nameof(userName));

            if (string.IsNullOrWhiteSpace(userPassword))
                throw new ArgumentException(nameof(userPassword));

            var user = await _applicationDbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
                return null;

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, userPassword, false);

            if (signInResult.Succeeded)
                return _mapper.Map<User>(user);
            else
                return null;
        }

        /// <summary>
        /// Получить список всех пользователей.
        /// </summary>
        /// <returns>Пользователи системы.</returns>
        public async Task<User[]> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();

            return _mapper.Map<Core.Models.User[]>(users);
        }
    }
}