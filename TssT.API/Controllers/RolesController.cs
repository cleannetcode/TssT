using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IRoleService _roleService;

        public RolesController(IMapper mapper,IRoleService roleService)
        {
            _mapper = mapper;
            _roleService = roleService;
        }

        /// <summary>
        /// Получить список всех доступных ролей.
        /// </summary>
        /// <returns>Список ролей.</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var allRoles = await _roleService.GetAll();
            var result = _mapper.Map<Role[]>(allRoles);
            return Ok(result);
        }

        /// <summary>
        /// Создать новую роль.
        /// </summary>
        /// <param name="newRole">Имя роли.</param>
        /// <returns>Созданная роль.</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(NewRole newRole)
        {
            var createdRole = await _roleService.Create(newRole.Name);
            var result = _mapper.Map<Role>(createdRole);
            return Ok(result);
        }

        /// <summary>
        /// Удалить роль по идентификатору.
        /// </summary>
        /// <param name="roleId">Идентификатор роли.</param>
        /// <returns>Результат удаления. True-успешно, False-неуспешно.</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> DeleteById(string roleId)
        {
            var result = await _roleService.DeleteById(roleId);
            return Ok(result);
        }
    }
}
