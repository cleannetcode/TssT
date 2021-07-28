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
        public async Task<Role[]> GetAll()
        {
            var allRoles = await _roleService.GetAll();

            return _mapper.Map<Role[]>(allRoles);
        }

        [HttpPost("[action]")]
        public async Task<Role> Create(NewRole newRole)
        {
            var createdRole = await _roleService.Create(newRole.Name);
            return _mapper.Map<Role>(createdRole);
        }

    }
}
