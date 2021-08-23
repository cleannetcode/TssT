using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TssT.API.Contracts;
using TssT.Core.Interfaces;

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
        private readonly UserManager<DataAccess.Entities.User> _userManager;
        private readonly RoleManager<DataAccess.Entities.Role> _roleManager;

        public UserRoleController(
            IMapper mapper, 
            UserManager<DataAccess.Entities.User> userManager, 
            RoleManager<DataAccess.Entities.Role> roleManager)
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
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest();

            IList<string> rolesIList = await _userManager.GetRolesAsync(user);
            var roles = new List<string>(rolesIList);
            return Ok(roles);
        }
        
        /// <summary>
        /// Добавить роль для пользователя.
        /// </summary>
        /// <param name="userRole">Идентификатор роли и пользователя.</param>
        /// <returns>Результат операции. True-успешно, False-неуспешно.</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> AddRoleToUser(UserRole userRole)
        {
            var user = await _userManager.FindByIdAsync(userRole.UserId);
            if (user == null)
                return BadRequest();

            var role = await _roleManager.FindByIdAsync(userRole.RoleId);
            if (role == null)
                return BadRequest();

            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
                return Ok();
            else
                return BadRequest();
        }
        
        /// <summary>
        /// Снять роль пользователю.
        /// </summary>
        /// <param name="userRole">Идентификатор роли и пользователя.</param>
        /// <returns>Результат операции Ok или BadRequest.</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> RemoveRoleFromUser(UserRole userRole)
        {
            var user = await _userManager.FindByIdAsync(userRole.UserId);
            if (user == null)
                return BadRequest("User not found.");

            var role = await _roleManager.FindByIdAsync(userRole.RoleId);
            if (role == null)
                return BadRequest("Role not found");

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (result.Succeeded)
                return Ok();
            else
                return BadRequest();
        }
    }
}
