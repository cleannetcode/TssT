using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IUserService _userService;

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            Core.Models.User[] allUsers = await _userService.GetAll();
            var result = _mapper.Map<User[]>(allUsers);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(NewUser user)
        {
            Core.Models.User userForCreate = new Core.Models.User()
            {
                UserName = user.UserName,
                Email = user.Email
            };

            Core.Models.User createdUser = await _userService.Create(userForCreate, user.Password);
            var result = _mapper.Map<User>(createdUser);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> DeleteById(string userId)
        {
            bool result = await _userService.DeleteById(userId);
            return Ok(result);
        }
        // Добавить права (role) для пользователя


    }
}
