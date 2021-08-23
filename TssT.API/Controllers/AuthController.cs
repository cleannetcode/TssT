using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using TssT.API.Contracts;

namespace TssT.API.Controllers
{
    /// <summary>
    /// Контроллер авторизации.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<DataAccess.Entities.User> _userManager;
        private readonly SignInManager<DataAccess.Entities.User> _signInManager;

        public AuthController(IMapper mapper,
            UserManager<DataAccess.Entities.User> userManager,
            SignInManager<DataAccess.Entities.User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Получить токен для доступа к API.
        /// </summary>
        /// <param name="userCredential">Учетные данные пользователя.</param>
        /// <returns>Bearer токен.</returns>
        [HttpPost("/token")]
        public async Task<IActionResult> Token(UserCredential userCredential)
        {
            var user = await GetByNameAndPassword(userCredential.Name, userCredential.Password);

            if (user != null)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);

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

        /// <summary>
        /// Получить пользователя по имени и паролю.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="userPassword">Пароль пользователя.</param>
        /// <returns>Возвращает пользователя.</returns>
        private async Task<DataAccess.Entities.User> GetByNameAndPassword(string userName, string userPassword)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return null;

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, userPassword, false);

            if (signInResult.Succeeded)
                return user;
            else
                return null;
        }
    }
}
