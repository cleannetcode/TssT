using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TssT.Core.Interfaces;
using TssT.Core.Models;

namespace TssT.DataAccess.Repositories
{
    /// <summary>
    /// Класс репозиторий для ролей пользователя.
    /// </summary>
    public class UserRoleRepository
    {
        private readonly IMapper _mapper;
        private readonly UserManager<Entities.User> _userManager;
        private readonly RoleManager<Entities.Role> _roleManager;

        public UserRoleRepository(IMapper mapper,
            UserManager<Entities.User> userManager,
            RoleManager<Entities.Role> roleManager
        )
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Получить роли пользователя.
        /// </summary>
        /// <param name="user">Пользователь чьи роли необходимо получить.</param>
        /// <returns>Список ролей.</returns>
        public async Task<List<string>> GetUserRoles(User user)
        {
            var userForSearch = _mapper.Map<Entities.User>(user);

            IList<string> roles = await _userManager.GetRolesAsync(userForSearch);

            return new List<string>(roles);
        }

        /// <summary>
        /// Добавить роль пользователю.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="roleId">Имя роли.</param>
        /// <returns></returns>
        public async Task<bool> AddRoleToUser(string userId, string roleId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return false;

            var result = await _userManager.AddToRoleAsync(user, role.Name);
            return result.Succeeded;
        }
    }
}
