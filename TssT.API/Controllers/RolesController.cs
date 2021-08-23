using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TssT.API.Contracts;
using TssT.Core.Interfaces;

namespace TssT.API.Controllers
{
    /// <summary>
    /// Контроллер для управления ролями.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly RoleManager<DataAccess.Entities.Role> _roleManager;

        public RolesController(IMapper mapper, RoleManager<DataAccess.Entities.Role> roleManager)
        {
            _mapper = mapper;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Получить список всех доступных ролей.
        /// </summary>
        /// <returns>Список ролей.</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var allRoles = await _roleManager.Roles.ToListAsync();
            return Ok(allRoles);
        }

        /// <summary>
        /// Создать новую роль.
        /// </summary>
        /// <param name="newRole">Имя роли.</param>
        /// <returns>Созданная роль.</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(NewRole newRole)
        {
            /*var createdRole = await _roleService.Create(newRole.Name);
            var result = _mapper.Map<Role>(createdRole);
            return Ok(result);*/

            DataAccess.Entities.Role newRoleEntity = new DataAccess.Entities.Role(newRole.Name) { Id = Guid.NewGuid().ToString() };

            var result = await _roleManager.CreateAsync(newRoleEntity);

            if (result.Succeeded)
            {
                var createdRole = await _roleManager.FindByNameAsync(newRole.Name);

                return Ok(createdRole);
            }
            else
                return BadRequest();
        }

        /// <summary>
        /// Удалить роль по идентификатору.
        /// </summary>
        /// <param name="roleId">Идентификатор роли.</param>
        /// <returns>Результат удаления. True-успешно, False-неуспешно.</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> DeleteById(string roleId)
        {
            var roleForDelete = await _roleManager.FindByIdAsync(roleId);

            var result = await _roleManager.DeleteAsync(roleForDelete);

            if (result.Succeeded)
                return Ok();
            else
                return BadRequest();
        }
    }
}
