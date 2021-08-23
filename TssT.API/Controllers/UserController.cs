using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TssT.API.Contracts;
using TssT.Core.Interfaces;

namespace TssT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        //private readonly IUserService _userService;
        private readonly UserManager<DataAccess.Entities.User> _userManager;

        public UserController(IMapper mapper, 
            //IUserService userService, 
            UserManager<DataAccess.Entities.User> userManager)
        {
            _mapper = mapper;
            //_userService = userService;
            _userManager = userManager;
        }

        /// <summary>
        /// Получить список всех пользователей.
        /// </summary>
        /// <returns>Пользователи системы.</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();

            return Ok(users);
        }

        /// <summary>
        /// Создать пользователя.
        /// </summary>
        /// <param name="user">Пользователь для создания.</param>
        /// <returns>Созданный пользователь или null.</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(NewUser user)
        {
            DataAccess.Entities.User userForCreate = new DataAccess.Entities.User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = user.UserName,
                Email = user.Email
            };
            IdentityResult result = await _userManager.CreateAsync(userForCreate, user.Password);

            if (result.Succeeded)
                return Ok(userForCreate);
            else
                return BadRequest();    //выбрать тип для ошибки во время создания пользователя.
        }

        /// <summary>
        /// Удалить пользователя по Id.
        /// </summary>
        /// <param name="userId">Id пользователя.</param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> DeleteById(string userId)
        {
            //bool result = await _userService.DeleteById(userId);
            var userForDelete = await _userManager.FindByIdAsync(userId);
            if (userForDelete == null)
                return BadRequest("User not found");

            var result = await _userManager.DeleteAsync(userForDelete);
            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest();
        }
    }
}
