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
    public class RolesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public RolesController(IMapper mapper,IRoleService roleService)
        {
            _mapper = mapper;
            _roleService = roleService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var allRoles = await _roleService.GetAll();
            var result = _mapper.Map<Role[]>(allRoles);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(NewRole newRole)
        {
            var createdRole = await _roleService.Create(newRole.Name);
            var result = _mapper.Map<Role>(createdRole);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> DeleteById(string roleId)
        {
            var result = await _roleService.DeleteById(roleId);
            return Ok(result);
        }
    }
}
