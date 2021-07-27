using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TssT.Core.Interfaces;

namespace TssT.DataAccess.Repositories
{
    /// <summary>
    /// Класс репозиторий для доступа к Ролям
    /// </summary>
    public class RoleRepository : IRoleRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly RoleManager<Entities.Role> _roleManager;
        private readonly UserManager<Entities.User> _userManager;

        public RoleRepository(IMapper mapper,
            ApplicationDbContext applicationDbContext,
            RoleManager<Entities.Role> roleManager,
            UserManager<Entities.User> userManager
        )
        {
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Взять список всех доступных ролей.
        /// </summary>
        /// <returns>Список ролей.</returns>
        public async Task<Core.Models.Role[]> GetAll()
        {
            // _roleManager.Roles.ToListAsync(); не работает. почему ?
            var allRoles = await _applicationDbContext.Roles.ToListAsync();
            var result = _mapper.Map<Core.Models.Role[]>(allRoles);
            return result;
        }

        /// <summary>
        /// Обновить имя роли.
        /// </summary>
        /// <param name="roleId">Идентификатор роли.</param>
        /// <param name="newRoleName">Новое имя роли.</param>
        /// <returns>Результат обновления true, false.</returns>
        public async Task<bool> Update(string roleId, string newRoleName)
        {
            if (String.IsNullOrWhiteSpace(roleId))
                throw new ArgumentException(nameof(roleId));

            if (String.IsNullOrWhiteSpace(newRoleName))
                throw new ArgumentException(nameof(newRoleName));

            var roleForUpdate = await _roleManager.FindByIdAsync(roleId);

            if (roleForUpdate == null)
                return false;

            roleForUpdate.Name = newRoleName;

            var result = await _roleManager.UpdateAsync(roleForUpdate);

            return result.Succeeded;
        }

        /// <summary>
        /// Удалить роль по Id.
        /// </summary>
        /// <param name="roleId">Id роли.</param>
        /// <returns>Результат удаления true, false.</returns>
        public async Task<bool> Delete(string roleId)
        {
            var roleForDelete = await _roleManager.FindByIdAsync(roleId);
            if (roleForDelete == null)
                return false;

            var result = await _roleManager.DeleteAsync(roleForDelete);

            return result.Succeeded;
        }

        /// <summary>
        /// Создать новую роль.
        /// </summary>
        /// <param name="roleName">Имя роли.</param>
        /// <returns>Созданная роль.</returns>
        public async Task<Core.Models.Role> Create(string roleName)
        {
            if (String.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException(nameof(roleName));

            Entities.Role newRole = new Entities.Role(roleName) { Id = Guid.NewGuid().ToString() };

            var result = await _roleManager.CreateAsync(newRole);

            if (result.Succeeded)
            {
                var createdRole = await _roleManager.FindByNameAsync(roleName);

                return _mapper.Map<Core.Models.Role>(createdRole);
            }
            else
                return null;
        }
    }
}
