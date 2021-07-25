using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        public async Task<IActionResult> Create(API.Contracts.NewUser newUser)
        {
            var user = _mapper.Map<API.Contracts.NewUser, Core.Models.User>(newUser);
            try
            {
                await _usersManagerService.Create(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok(user.Id);
        }
        
        /// <summary>
        /// Update user's field
        /// </summary>
        /// <param name="user"></param>
        /// <returns>bool</returns>
        [HttpPut("update")]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(API.Contracts.User user)
        {
            try
            {
                await _usersManagerService.Update(_mapper.Map<API.Contracts.User, Core.Models.User>(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("deletebyuserid")]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteByUserId(string userId)
        {
            IdentityResult result = null;
            try
            {
                result = await _usersManagerService.DeleteById(userId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
            return Ok(result);
        }
        
        
        /// <summary>
        /// Delete user by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpDelete("deletebyusername")]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteByUserName(string userName)
        {
            IdentityResult result = null;
            try
            {
                result = await _usersManagerService.DeleteByUserName(userName);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
            return Ok(result);
        }        

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("getbyid")]
        [ProducesResponseType(typeof(API.Contracts.User), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(API.Contracts.User), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetById(string userId)
        {
            API.Contracts.User user = null;
            try
            {
                user = _mapper.Map<Core.Models.User, API.Contracts.User>( await _usersManagerService.GetById(userId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
            return Ok(user);
        }
        
        /// <summary>
        /// Get user by username
        /// </summary>
        /// <param name="userUserName"></param>
        /// <returns></returns>
        [HttpGet("getbyusername")]
        [ProducesResponseType(typeof(API.Contracts.User), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(API.Contracts.User), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByUserName(string username)
        {
            API.Contracts.User user = null;
            try
            {
                user = _mapper.Map<Core.Models.User, API.Contracts.User>( await _usersManagerService.GetByUserName(username));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
            return Ok(user);
        }
    }
}