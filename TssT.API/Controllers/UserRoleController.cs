using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TssT.API.Contracts;
using Role = TssT.DataAccess.Entities.Role;
using User = TssT.DataAccess.Entities.User;

namespace TssT.API.Controllers
{
    /// <summary>
    /// Контроллер для доступа к ролям пользователей.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserRoleController(
            IMapper mapper, 
            UserManager<User> userManager, 
            RoleManager<Role> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Вывести все роли пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Список ролей.</returns>
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(List<String>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            if (userId == null)
                return BadRequest(nameof(userId)+" is not set.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest("User not found.");

            IList<string> rolesIList = await _userManager.GetRolesAsync(user);
            var roles = new List<string>(rolesIList);
            return Ok(roles);
        }

        /// <summary>
        /// Добавить роль для пользователя.
        /// </summary>
        /// <param name="userRole">Идентификатор роли и пользователя.</param>
        /// <returns>Результат операции.</returns>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddRoleToUser(UserRole userRole)
        {
            var user = await _userManager.FindByIdAsync(userRole.UserId);
            if (user == null)
                return BadRequest("User not found.");

            var role = await _roleManager.FindByIdAsync(userRole.RoleId);
            if (role == null)
                return BadRequest("Role not found.");

            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
                return Ok();
            else
                return BadRequest("Role not added to user.");
        }

        /// <summary>
        /// Снять роль с пользователя.
        /// </summary>
        /// <param name="userRole">Идентификатор роли и пользователя.</param>
        /// <returns>Результат операции.</returns>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveRoleFromUser(UserRole userRole)
        {
            var user = await _userManager.FindByIdAsync(userRole.UserId);
            if (user == null)
                return BadRequest("User not found.");

            var role = await _roleManager.FindByIdAsync(userRole.RoleId);
            if (role == null)
                return BadRequest("Role not found.");

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (result.Succeeded)
                return Ok();
            else
                return BadRequest("Role not removed from user.");
        }
    }
}
