using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TssT.API.Contracts;
using Role = TssT.DataAccess.Entities.Role;

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
        private readonly RoleManager<Role> _roleManager;

        public RolesController(IMapper mapper, RoleManager<Role> roleManager)
        {
            _mapper = mapper;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Получить список всех доступных ролей.
        /// </summary>
        /// <returns>Список ролей.</returns>
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(List<Contracts.Role>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var allRoles = await _roleManager.Roles.ToListAsync();
            var contractRoles = _mapper.Map<List<Contracts.Role>>(allRoles);
            return Ok(contractRoles);
        }

        /// <summary>
        /// Создать новую роль.
        /// </summary>
        /// <param name="newRole">Имя роли.</param>
        /// <returns>Созданная роль.</returns>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Contracts.Role), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Contracts.Role), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(NewRole newRole)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newRoleEntity = new Role(newRole.Name) { Id = Guid.NewGuid().ToString() };

            var result = await _roleManager.CreateAsync(newRoleEntity);

            if (result.Succeeded)
            {
                var createdRole = await _roleManager.FindByNameAsync(newRole.Name);
                var contractRole = _mapper.Map<Contracts.Role>(createdRole);
                return Ok(contractRole);
            }
            else
                return BadRequest("Role not created.");
        }

        /// <summary>
        /// Удалить роль по идентификатору.
        /// </summary>
        /// <param name="roleId">Идентификатор роли.</param>
        /// <returns>Результат удаления. True-успешно, False-неуспешно.</returns>
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteById(string roleId)
        {
            if (String.IsNullOrWhiteSpace(roleId))
                return BadRequest(nameof(roleId) + " is not set");

            var roleForDelete = await _roleManager.FindByIdAsync(roleId);
            if (roleForDelete == null)
                return BadRequest("Role not found");

            var result = await _roleManager.DeleteAsync(roleForDelete);

            if (result.Succeeded)
                return Ok();
            else
                return BadRequest("Role not deleted");
        }
    }
}
