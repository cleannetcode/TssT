﻿using Microsoft.AspNetCore.Http;
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
    public class AuthController : ControllerBase
    {
        private readonly IUserService _accountService;
        private readonly IRoleService _roleService;

        public AuthController(IUserService accountService,IRoleService roleService)
        {
            _accountService = accountService;
            _roleService = roleService;
        }

        [HttpPost("/token")]
        public async Task<IActionResult> Token(UserCredential userCredential)
        {
            /*var user = await _accountService.GetByNameAndPassword(userCredential.Name, userCredential.Password);

            if (user != null )
            {*/
                //TODO: get role from database
                var role = "Admin";
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, userCredential.Name), // ClaimTypes.Name
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
                };

                var symmetricSecurityKey = AuthOptions.GetSymmetricSecurityKey();

                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256),
                    Audience = AuthOptions.AUDIENCE,
                    Issuer = AuthOptions.ISSUER
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { Token = tokenString });
           /* }
            else
            {
                return Unauthorized("failed, try again");
            }*/
        }
    }
}
