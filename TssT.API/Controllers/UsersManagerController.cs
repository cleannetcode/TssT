using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TssT.Core.Interfaces;

namespace TssT.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersManagerController : ControllerBase
    {
        private readonly IUsersManagerService _usersManagerService;
        private readonly IMapper _mapper;

        public UsersManagerController(IUsersManagerService usersManagerService, IMapper mapper)
        {
            _usersManagerService = usersManagerService;
            _mapper = mapper;
        }

        /// <summary>
        /// Create new member
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns>user.id</returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(Contracts.NewUser newUser)
        {
            var user = _mapper.Map<Contracts.NewUser, Core.Models.User>(newUser);
            await _usersManagerService.Create(user);

            return Ok(user.Id);
        }
        
        /// <summary>
        /// Update user's field
        /// </summary>
        /// <param name="user"></param>
        /// <returns>bool</returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPut("update")]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(Contracts.User user)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>bool</returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpDelete("delete")]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(Contracts.User user)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("getbyid")]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public Task<Contracts.User> GetById(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}