using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IMapper mapper, IUserRoleService userRoleService)
        {
            _mapper = mapper;
            _userRoleService = userRoleService;
        }

        /// <summary>
        /// Вывести все роли пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Список ролей.</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            var result = await _userRoleService.GetUserRoles(userId);
            return Ok(result);
        }

        /// <summary>
        /// Добавить роль для пользователя.
        /// </summary>
        /// <param name="userRole">Идентификатор роли и пользователя.</param>
        /// <returns>Результат операции. True-успешно, False-неуспешно.</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> AddRoleToUser(UserRole userRole)
        {
            var result = await _userRoleService.AddRoleToUser(userRole.UserId, userRole.RoleId);
            return Ok(result);
        }

        /// <summary>
        /// Снять роль пользователю.
        /// </summary>
        /// <param name="userRole">Идентификатор роли и пользователя.</param>
        /// <returns>Результат операции. True-успешно, False-неуспешно.</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> RemoveRoleFromUser(UserRole userRole)
        {
            var result = await _userRoleService.RemoveRoleFromUser(userRole.UserId, userRole.RoleId);
            return Ok(result);
        }
    }
}
