using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TssT.API.Contracts;
using User = TssT.DataAccess.Entities.User;

namespace TssT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserController(IMapper mapper, 
            UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        /// <summary>
        /// Получить список всех пользователей.
        /// </summary>
        /// <returns>Пользователи системы.</returns>
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(List<Contracts.User>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();
            var contractUsers = _mapper.Map<List<Contracts.User>>(users);
            return Ok(contractUsers);
        }

        /// <summary>
        /// Создать пользователя.
        /// </summary>
        /// <param name="user">Пользователь для создания.</param>
        /// <returns>Созданный пользователь или null.</returns>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Contracts.User), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(NewUser user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));

            User userForCreate = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = user.UserName,
                Email = user.Email
            };
            IdentityResult result = await _userManager.CreateAsync(userForCreate, user.Password);
            var contractUser = _mapper.Map<Contracts.User>(userForCreate);

            if (result.Succeeded)
                return Ok(contractUser);
            else
                return BadRequest("User not created.");
        }

        /// <summary>
        /// Удалить пользователя по Id.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteById(string userId)
        {
            if (String.IsNullOrWhiteSpace(userId))
                return BadRequest(nameof(userId) + " is not set");

            var userForDelete = await _userManager.FindByIdAsync(userId);
            if (userForDelete == null)
                return BadRequest("User not found");

            var result = await _userManager.DeleteAsync(userForDelete);

            if (result.Succeeded)
                return Ok();
            else
                return BadRequest("User not deleted");
        }
    }
}
