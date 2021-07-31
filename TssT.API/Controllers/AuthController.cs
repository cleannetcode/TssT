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
    /// <summary>
    /// Контроллер авторизации.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _accountService;
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;

        public AuthController(IUserService accountService, IRoleService roleService, IUserRoleService userRoleService)
        {
            _accountService = accountService;
            _roleService = roleService;
            _userRoleService = userRoleService;
        }

        /// <summary>
        /// Получить токен для доступа к API.
        /// </summary>
        /// <param name="userCredential">Учетные данные пользователя.</param>
        /// <returns>Bearer токен.</returns>
        [HttpPost("/token")]
        public async Task<IActionResult> Token(UserCredential userCredential)
        {
            var user = await _accountService.GetByNameAndPassword(userCredential.Name, userCredential.Password);

            if (user != null)
            {
                var roles = await _userRoleService.GetUserRoles(user.Id);
                var rolesString = String.Join(",", roles);
                
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, userCredential.Name),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, rolesString)
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
            }
            else
            {
                return Unauthorized("Authorize is failed.");
            }
        }
    }
}
