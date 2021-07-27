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
    public class UserRoleRepository: IUserRoleRepository
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
        public async Task<List<string>> GetUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            IList<string> roles = await _userManager.GetRolesAsync(user);

            return new List<string>(roles);
        }

        /// <summary>
        /// Добавить роль пользователю.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="roleId">Идентификатор роли.</param>
        /// <returns>Результат добавления. True-успешно, False-неуспешно.</returns>
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

        /// <summary>
        /// Снять роль с пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="roleId">Идентификатор роли.</param>
        /// <returns>Результат удаления. True-успешно, False-неуспешно.</returns>
        public async Task<bool> RemoveRoleFromUser(string userId, string roleId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return false;

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            return result.Succeeded;
        }
    }
}
